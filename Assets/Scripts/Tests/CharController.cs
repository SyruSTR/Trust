using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    //ссылка на компонент - CrahacteerController
    public static CharacterController unityController;

    //ссылка на себя - на класс CharController
    public static CharController instance;

    //Результат - вектор движения.
    public Vector3 move;

    //Размер мёртвой зоны
    private const float _DEAD_ZONE = 0.1f;

    void Awake()
    {
        //Запоминаем ссылку на себя
        instance = this;

        //Находим компонент - CharacterController
        unityController = GetComponent("CharacterController") as CharacterController;

        //Просим у класса камеры найти камеру в сцене
       // CharTPSCamera.GetCamera();
    }




    void Update () {
        // Если камеры нет -ничего не делаем
        if (Camera.main == null) return;

        //Обрабатываем введенные игроком данные
        GetInput();

        //Говорим CharMotor, что пора двигаться
        CharMotor.instance.UpdateMotor(move);
	}

    private void GetInput()
    {
        float vert = Input.GetAxis("Vertical");

        float horiz = Input.GetAxis("Horizontal");

        move = Vector3.zero;

        if (vert > _DEAD_ZONE || vert < _DEAD_ZONE)
            move += new Vector3(0,0, vert);

        if (horiz > _DEAD_ZONE || horiz < _DEAD_ZONE)
            move += new Vector3(horiz,0,0);
    }
}
