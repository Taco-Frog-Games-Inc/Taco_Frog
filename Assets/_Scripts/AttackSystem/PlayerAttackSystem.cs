using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackSystem
{

    protected readonly float _damage;

    private JumpAttack jAttack;

    public PlayerAttackSystem(GameObject en, GameObject pl)
    {

        jAttack = new JumpAttack(en, pl);

    }

    public void JumpAttack()
    {
        jAttack.ShortRangeAttack();
    }


}


internal class JumpAttack
{
    private GameObject enemy;
    private GameObject player;

    public JumpAttack(GameObject en, GameObject pl)
    {
        enemy = en;
        player = pl;
    }

    private bool IsPlayerAboveEnemy()
    {
        float playerY = player.transform.position.y;
        float enemyY = enemy.transform.position.y;
        float threshold = 0.5f;
        return playerY > enemyY + threshold;
    }

    private void CheckForEnemyBelow()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Player is Above!!!");
                var enemy = hit.collider.gameObject;
                if (enemy != null)
                {
                    if (IsPlayerAboveEnemy())
                    {
                        Debug.Log("Player is Above!!!");
                        enemy.SetActive(false);
                    }
                }
            }
        }
    }

    public void ShortRangeAttack()
    {
        CheckForEnemyBelow();
    }

}



internal class TongueAttack
{
    private GameObject tongue;
    private Transform _mouthTransform;
    private float extensionDistance = 3.0f;
    private float speed = 2.0f;
    private bool isAttacking = false;
    private float currentScaleZ;
    private float targetScaleZ;

    public void Initialize(GameObject tonguePrefab, Transform mouthTransform)
    {
        tongue = Object.Instantiate(tonguePrefab, mouthTransform.position, Quaternion.identity);
        tongue.transform.SetParent(mouthTransform);
        _mouthTransform = mouthTransform;
        currentScaleZ = tongue.transform.localScale.z;
        targetScaleZ = currentScaleZ;
    }

    public void Update()
    {
        if (isAttacking)
        {

            if (currentScaleZ < targetScaleZ)
            {
                currentScaleZ += speed * Time.deltaTime;
                tongue.transform.localScale = new Vector3(tongue.transform.localScale.x, tongue.transform.localScale.y, currentScaleZ);
            }

            else if (currentScaleZ > targetScaleZ)
            {
                currentScaleZ -= speed * Time.deltaTime;
                tongue.transform.localScale = new Vector3(tongue.transform.localScale.x, tongue.transform.localScale.y, currentScaleZ);
            }

            if (currentScaleZ >= targetScaleZ && targetScaleZ == currentScaleZ)
            {

                targetScaleZ = tongue.transform.localScale.z;
                targetScaleZ = tongue.transform.localScale.z;
                isAttacking = false;
            }
        }
    }

    public void ExecuteAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            targetScaleZ = tongue.transform.localScale.z + extensionDistance;
        }
    }
}