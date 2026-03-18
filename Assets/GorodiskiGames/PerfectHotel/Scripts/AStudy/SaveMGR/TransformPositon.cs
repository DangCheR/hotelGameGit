using UnityEngine;

class TransformPosition : MonoBehaviour
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Camera _camera;
    public TransformPosition(Vector3 position, Quaternion rotation)
    {
        _camera = Camera.main;
        Position = position;
        Rotation = rotation;
    }
    //转换为世界坐标
    public Vector3 TransformPoint(){
        Vector3 transformedPosition = transform.TransformPoint(Position);
        return transformedPosition;
    }
    //转换为局部坐标
    public Vector3 InverseTransformPoint(Vector3 worldPosition){
        Vector3 transformedPosition = transform.InverseTransformPoint(worldPosition);
        return transformedPosition;
    }
    //转换为世界坐标的方向
    public Vector3 TransformDirection(){
        Vector3 transformedDirection = transform.TransformDirection(Position);
        return transformedDirection;
    }
    //转换为局部坐标的方向
    public Vector3 InverseTransformDirection(Vector3 worldDirection){
        Vector3 transformedDirection = transform.InverseTransformDirection(worldDirection);
        return transformedDirection;
    }
    //转换为世界坐标的向量，方向不变
    public Vector3 TransformVector(){
        Vector3 transformedVector = transform.TransformVector(Position);
        return transformedVector;
    }
    //转换为局部坐标的向量，方向不变
    public Vector3 InverseTransformVector(Vector3 worldVector){
        Vector3 transformedVector = transform.InverseTransformVector(worldVector);
        return transformedVector;
    }

    //世界坐标转换为屏幕坐标
    public Vector3 WorldToScreenPoint(Vector3 worldPosition){
        Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);
        return screenPosition;
    }
    //屏幕坐标转换为世界坐标
    public Vector3 ScreenToWorldPoint(Vector3 screenPosition){
        Vector3 worldPosition = _camera.ScreenToViewportPoint(screenPosition);
        return worldPosition;
    }
    //屏幕坐标转换为视口坐标
    public Vector3 ScreenToViewportPoint(Vector3 screenPosition){
        Vector3 viewportPosition = _camera.ScreenToViewportPoint(screenPosition);
        return viewportPosition;
    }
    public Vector3 WorldToViewportPoint(Vector3 worldPosition){
        Vector3 viewportPosition = _camera.WorldToViewportPoint(worldPosition);
        return viewportPosition;
    }
}