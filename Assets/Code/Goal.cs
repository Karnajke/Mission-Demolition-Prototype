using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{ // —татическое поле доступное любому другому коду
    static public bool goalMet = false;

    void OnTriggerEnter(Collider other)
    { //  огда в область действи€ тригера поподает что-то,
      // проверить, €вл€етс€ ли это "что-то" снар€дом
      if (other.gameObject.tag == "Projectile")
        {// если это снар€д, то присвоить полю goalMet значение true
            Goal.goalMet = true;
            // “акже изменить альфа канал цвета, чтобы увеличить не прозрачность
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;

        }

    }
    
}
