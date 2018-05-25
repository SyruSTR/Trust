using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharTPSCamera : MonoBehaviour {

    public static CharTPSCamera instance;
    public Transform target;

    public float Distance { get; set; }


    //Минимальное расстояние от камеры до персонажа
    private const float _DISTANCE_MIN = 2f;

    //Начальное расстояние от камеры до персонажа. У меня равно минимальному. Вы можете выбрать любое от минимального до максимального
    private const float _START_DISTANCE = _DISTANCE_MIN;

    //Максимально расстояние от камеры до персонажа
    private const float _DISTANCE_MAX = 12f;

    //Параметр смягчения приближения/удаления камеры
    private const float _DISTANCE_SMOTH = 0.05f;
    private const float _X_SMOTH = 0.05f;
    private const float _Y_SMOTH = 0.1f;

    //Текущая скорость приближения/удаления камеры
    private float _velDistance;

    //Значение смещения мыши по оси X
    private float _mouseX;

    //Значение смещения мыши по оси Y
    private float _mouseY;

    //Рассчитанное желаемое расстояние от камеры до персонажа
    private float _desireDistance;

    //Полученное желаемое положение камеры
    private Vector3 _desirePosition;

    //Константы - чувствительность мыши в горизонтальном, вертикальном направлениях и чувствительность колесика
    private const float _X_MOUSE_SENSITIVITY = 5f;
    private const float _Y_MOUSE_SENSITIVITY = 5f;
    private const float _MOUSE_WHEEL_SENSITIVITY = 5f;

    //"Мертвая зона" внутри которой не реагируем на вращение колесика мышки
    private const float _DEAD_ZONE = 0.01f;

    //Ограничения вращения по вертикали - минимальное и максимальное
    private const float _Y_MIN_LIMIT = -40f;
    private const float _Y_MAX_LIMIT = 80f;



    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Distance = Mathf.Clamp(Distance, _DISTANCE_MIN, _DISTANCE_MAX);
        //Вызываем метод, устанавливающий начальные значения переменных.
        Reset();
    }

    void LateUpdate()
    {
        //Проверяем не исчезла лицель камеры, если исчезла ничего не делаем
        if (target == null) return;

        //Вводим данные с мыши.
        PlayerInput();

        //Рассчитываем желаемую позицию камеры
        CalcDesipePosition();

        //Смещаем камеру
        UpdatePosition();
    }

    private void PlayerInput()
    {
        //Проеряем - нажата ли правая кнопка мыши
        if (Input.GetMouseButtonDown(1))
        {
            //Нажата - рассчитываем смещения с учетом чувствительности мыши
            _mouseX += Input.GetAxis("Mouse X") * _X_MOUSE_SENSITIVITY;
            _mouseY += Input.GetAxis("Mouse Y") * _Y_MOUSE_SENSITIVITY;
        }

        //Ограничиваем вращение по вертикали с учетом того, что оно может выходить за пределы диапазона [-360;360]
        _mouseY = Utils.ClampAngle(_mouseY, _Y_MIN_LIMIT, _Y_MAX_LIMIT);

        //Данные с колесика мышки
        /*float scroll = Input.GetAxis("Mouse ScrollWhell");

        //Если вышли за пределы "мертвой зоны"
        if (scroll < -_DEAD_ZONE || scroll > _DEAD_ZONE)
        {
            //Рассчитываем желаемое расстояние от камеры до персонажа
            //Введенное значение умножаем на чувствительность, вычитаем его из текущего расстояния и ограничиваем сверху и снизу
            _desireDistance = Mathf.Clamp(Distance - scroll * _MOUSE_WHEEL_SENSITIVITY, _DISTANCE_MIN, _DISTANCE_MAX);
        }*/
    }

    //Рассчитываем желаемую позицию камеры
    private void CalcDesipePosition()
    {
        //"Смягчаем" приближение/удаление камеры
        Distance = Mathf.SmoothDamp(Distance, _desireDistance, ref _velDistance, _DISTANCE_SMOTH);

        //Собственно рассчитываем позицию. Обратите внимание на перекрестную передачу параметров
        //_mouseX и _mouseY
        _desirePosition = CalcPosition(_mouseY, _mouseX, Distance);
    }

    private Vector3 CalcPosition(float rotx, float roty, float distance)
    {
        //Точка прямо позади персонажа на расстоянии камеры
        Vector3 direction = new Vector3(0,0, -distance);

        //Поворот вокруг персонажа на нужный угол
        Quaternion rotation = Quaternion.Euler(rotx, roty, 0);

        //Возвращаем нужную позицию камеры в мировом пространстве
        return target.position + rotation * direction;
    }

    public void Reset()
    {
        //Обнуляем данные, введенные с мыши
        _mouseX = 0;
        _mouseY = 10f;

        //Расстояние от камеры до персонажа равно стартовому
        Distance = _START_DISTANCE;

        //Желаемое расстояние тоже равно стартовому
        _desireDistance = Distance;
    }

    private void UpdatePosition()
    {

    }

    static public void GetCamera()
    {
        GameObject tempCamera;
        GameObject targetTemp;
        CharTPSCamera myCamera;

        if (Camera.main != null) tempCamera = Camera.main.gameObject;
        else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent<Camera>();
            tempCamera.tag = "Main Camera";

        }

        tempCamera.AddComponent<CharTPSCamera>();
        myCamera = tempCamera.GetComponent<CharTPSCamera>();

        targetTemp = GameObject.Find("targetLookAt");

        if (targetTemp == null)
        {
            targetTemp = new GameObject("targetLookAt");
            targetTemp.transform.position = Vector3.zero;
        }

        myCamera.target = targetTemp.transform;
    }
}

//Новый вспомогательный статический класс
internal static class Utils
{
    //метод для ограничения угла поворота
    public static float ClampAngle(float angle, float min, float max)
    {
        //Делаем
        do
        {
            //Если угол меньше -360 прибавляем к нему 360
            if (angle < -360) angle += 360;

            //Если больше 360 - вычитем
            if (angle > 360) angle -= 360;

            //Пока он не окажется в диапазоне [-360;360]
        } while (angle < -360 || angle > 360);

        //Возвращаем ограничив сверху и снизу
        return Mathf.Clamp(angle, min, max);
    }
}