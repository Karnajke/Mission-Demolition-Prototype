using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40; //Число облаков
    public GameObject cloudPrefub; //Шаблон для облаков
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; // минимальный маштаб облака
    public float cloudScaleMax = 3; //Максимальный маштаб облака
    public float cloudSpeedMult = 0.5f; //Коэффициент скорости облака

    private GameObject[] cloudInstances;

    void Awake()
    {
        //Создать масив для хранения всех экземпляров облаков
        cloudInstances = new GameObject[numClouds];
        //Найти родительский игровой обьект CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");
        //Создать в цикле заданое количество облаков
        GameObject cloud;
        for (int i=0; i<numClouds; i++)
        { //Создать экземпляр CloudPrefab
            cloud = Instantiate<GameObject>(cloudPrefub);
            //Выбрать местоположение для облака
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //Маштабировать облако
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // Меньшие обла (сменьшим значением ScaleU) должны быть ближе
            // к земле
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //Меньшие облака должны быть дальше
            cPos.z = 100 - 90 * scaleU;
            // Применить полученные значения координат и масштаба к облаку
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //Сделать облако дочерним по отношению к anchor
            cloud.transform.SetParent(anchor.transform);
            //Добавить облако в масив cloudInstances
            cloudInstances[i] = cloud;
        }
    }

    void Update()
    {
        //Обойти в цикле все созданные облака
        foreach (GameObject cloud in cloudInstances)
        { //Получить маштаб и координаты облака
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //Увеличить скорость для ближних облаков
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            // Если облако сместилось слишком далеко влево...
            if (cPos.x <= cloudPosMin.x)
            {  //Переместить его далеко в право
                cPos.x = cloudPosMax.x;
            }
            // Применить новые координаты к облаку
            cloud.transform.position = cPos;
        }
    }
}
