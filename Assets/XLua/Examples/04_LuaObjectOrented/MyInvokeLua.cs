using UnityEngine;
using XLua;
using System;

namespace XLuaStudy
{
    public class MyEvent : EventArgs{
        public string name;
        public object value;
    }

    public class MyInvokeLua : MonoBehaviour
    {
        LuaEnv luaenv = new LuaEnv();

        [CSharpCallLua]
        public interface ICall
        {
            event EventHandler<MyEvent> PropertyChanged;
            int Add(int a, int b);
            int Mult { get; set; }

            object this[int index] { get; set; }    
        }

        [CSharpCallLua]
        public delegate ICall MyCallNew(int mult, params string[] args);

        private string scripts = @"
            local calll_mt = {
                __index = {
                    Add = function(self, a, b)
                        return (a + b) * self.Mult
                    end,
                    get_Item = function(self, index)
                        return self.list[index + 1]
                    end,
                    set_Item = function(self, index, value)
                        self.list[index + 1] = value
                        self:notify({name = index, value = value})
                    end,
                    add_PropertyChanged = function(self, delegate)
                        if self.notifylist == nil then
                            self.notifylist = {}
                        end
                        table.insert(self.notifylist, delegate)
                    end,
                    remove_PropertyChanged = function(self, delegate)
                        if self.notifylist ~= nil then
                            for i=1, #self.notifylist do
                                if CS.System.Object.Equals(self.notifylist[i], delegate) then
                                    table.remove(self.notifylist, i)
                                    break
                                end
                            end
                        end
                    end,
                    notify = function(self, evt)
                        if self.notifylist ~= nil then
                            for i=1, #self.notifylist do
                                self.notifylist[i](self, evt)
                            end
                        end
                    end
                }
            }

            Call = {
                New = function(mult,...)
                    return setmetatable({Mult = mult, list = {'aaaa','bbbb','cccc'}}, calll_mt)
                end,
            }
        ";
        void Start()
        {
            exe(luaenv);
            luaenv.Dispose();
        }
        void exe(LuaEnv luaenv)
        {
            luaenv.DoString(scripts);
            MyCallNew call_new = luaenv.Global.GetInPath<MyCallNew>("Call.New");
            ICall call = call_new(10, "hi", "john");
            call[1] = "new value";
            call.PropertyChanged += Notify;
            call[1] = "new new value";
            call.PropertyChanged -= Notify;
            call[1] = "new new new value";
        }
        void Notify(object sender, MyEvent evt)
        {
            Debug.Log("事件被触发了，name=" + evt.name + ", value=" + evt.value);
        }
        
    }
}