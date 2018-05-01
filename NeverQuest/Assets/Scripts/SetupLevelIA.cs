//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SetupLevelIA : MonoBehaviour {

//    public  GameObject player, mob;

//    public float lvl1, lvl2, lvl3, lvl4, lvl5, lvl6;
//    bool flag_Stop;
//	// Use this for initialization
//	void Start () {
//        lvl1 = -14.1f;
//        lvl2 = -6f;
//        lvl3 = 2.8f;
//        lvl4 = lvl5 = 9.2f;
//        lvl6 = 12.2f;
//        flag_Stop = true;
//    }
	
//	// Update is called once per frame
//	void Update () {
        
//        if (flag_Stop) {
//            if (GetComponent<PlayerController>().isActiveAndEnabled) {
//                if (player.GetComponent<PlayerController>().transform.position.y <= lvl1) player.GetComponent<PlayerController>().transportLevel = 1;
//                else if (player.GetComponent<PlayerController>().transform.position.y <= lvl2 && player.GetComponent<PlayerController>().transform.position.y > lvl1) player.GetComponent<PlayerController>().transportLevel = 2;
//                else if (player.GetComponent<PlayerController>().transform.position.y <= lvl3 && player.GetComponent<PlayerController>().transform.position.y > lvl2) player.GetComponent<PlayerController>().transportLevel = 3;
//                else if (player.GetComponent<PlayerController>().transform.position.y <= lvl4 && player.GetComponent<PlayerController>().transform.position.y > lvl3) { print("TOU AQUI CRLH"); player.GetComponent<PlayerController>().transportLevel = 4; }
//                else if (player.GetComponent<PlayerController>().transform.position.x < 0 && player.GetComponent<PlayerController>().transform.position.y > lvl4) player.GetComponent<PlayerController>().transportLevel = 5;
//                else if (player.GetComponent<PlayerController>().transform.position.x > 0 && player.GetComponent<PlayerController>().transform.position.y > lvl4) player.GetComponent<PlayerController>().transportLevel = 6;
//            }
//            else if (GetComponent<MobController>().isActiveAndEnabled)
//            {
//                if (mob.GetComponent<MobController>().transform.position.y <= lvl1) mob.GetComponent<MobController>().mobTransportLevel = 1;
//                else if (mob.GetComponent<MobController>().transform.position.y <= lvl2 && mob.GetComponent<MobController>().transform.position.y > lvl1) mob.GetComponent<MobController>().mobTransportLevel = 2;
//                else if (mob.GetComponent<MobController>().transform.position.y <= lvl3 && mob.GetComponent<MobController>().transform.position.y > lvl2) mob.GetComponent<MobController>().mobTransportLevel = 3;
//                else if (mob.GetComponent<MobController>().transform.position.y <= lvl4 && mob.GetComponent<MobController>().transform.position.y > lvl3)
//                { print("TOU AQUI CRLH"); mob.GetComponent<MobController>().mobTransportLevel = 4; }
//                else if (mob.GetComponent<MobController>().transform.position.x < 0 && mob.GetComponent<MobController>().transform.position.y > lvl4) mob.GetComponent<MobController>().mobTransportLevel = 5;
//                else if (mob.GetComponent<MobController>().transform.position.x > 0 && mob.GetComponent<MobController>().transform.position.y > lvl4) mob.GetComponent<MobController>().mobTransportLevel = 6;
//            }
//            flag_Stop = false;
//        }
//    }
//}
