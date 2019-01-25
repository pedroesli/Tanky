using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [System.Serializable]
    public class Berserk
    {
        public enum State { Counting, Berserking, Finished };
        public int killsNeededForBeserkAbility;
        public float fireTimeDecrement;
        public float BeserkTime;
        public float movementSpeedIncreasment;
        public Transform shield;
        public Gun gun;
        public Movement movement;
        public CameraHype cameraHype;
        [HideInInspector] public State state = State.Counting;
        private void ShieldActive(bool state)
        {
            shield.gameObject.SetActive(state);
        }
        public  void BeserkMode()
        {
            MusicManager.instance.Play("Power_on");
            state = Berserk.State.Berserking;
            gun.ShootAbility(fireTimeDecrement);
            movement.MovementAbility(movementSpeedIncreasment);
            cameraHype.enabled = true;
            ShieldActive(true);
        }
        public void FinishBeserkMode()
        {
            MusicManager.instance.Play("Shutdown");
            state = Berserk.State.Counting;
            gun.ShootNormal();
            movement.NormalMovement();
            cameraHype.enabled = false;
            ShieldActive(false);
        }
    }
    
    public float life = 100;
    public int kills;
    public Transform gameOver;
    [Header("Beserk Ability")]
    public Berserk beserk;
    [HideInInspector]public float timer;
    
    void Update()
    {
        if(kills >= beserk.killsNeededForBeserkAbility && beserk.state == Berserk.State.Counting &&
            Input.GetKeyDown(KeyCode.F))
        {
            beserk.BeserkMode();
            timer = beserk.BeserkTime;
        }
        else if(beserk.state == Berserk.State.Berserking)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                beserk.state = Berserk.State.Finished;
            }
        }
        else if(beserk.state == Berserk.State.Finished)
        {
            beserk.FinishBeserkMode();
            kills = 0;
        }

        if (life <= 0)
        {
            // DEAD X_X
            MusicManager.instance.Play("Explosion");
            gameOver.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
