using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMotor : MonoBehaviour {

    public static CharMotor instance;
    public float speed = 10.0f;

    void Awake()
    {
        instance = this;
    }

    public void UpdateMotor(Vector3 move)
    {
        _RotateChar(move);
        _ProcessMotion(move);
    }

    public void _ProcessMotion(Vector3 moveVector)
    {
        //Преобразуем вектор движения в мировое пространство.
        moveVector = transform.TransformDirection(moveVector);
        //Нормализуем вектор движения
        Vector3.Normalize(moveVector);
        //Применяем скорость персонажа
        moveVector *= speed;
        //Переходим от кадров к секундам!
        moveVector *= Time.deltaTime;
        //Двигаем!
        CharController.unityController.Move(moveVector);
    }

    private void _RotateChar(Vector3 move)
    {
        //Проверяем - двигается ли персонаж?
        if (move.x != 0 || move.z != 0)
        {
            //Если двигается - уставливаем его поворот в соответствии с поворотом камеры. Т.к. это нужно сделать только вокруг оси Y,
            //а вокруг X и Z оставить неизменным, то используем метод, который конструирует вращение из трех углов
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
