using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : Character
{


    private Transform destination;

    //prywatne pola
    public bool isPurple;
    public float distance = 0.3f;
    private Vector2 target;
    private Transform greeportal;
    private Transform purpleportal;

    private string tagCoin = "Coins";
    private string tagPotion = "Potion";
    private string tagGems = "Gems";
    private string tagRange = "Range";
    private string tagSpeedPotion = "SpeedPotion";
    private string tagTrueAnswer = "TrueAnswer";
    private string tagFalseAnswer = "FalseAnswer";


    //nazwy, brakuje _? np _blocks i CamelCase
    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitpoints;

    private int exitIndex = 2;

    private SpellBook spellBook;
    //private Transform target;
    public Transform spawnPoint;
    // Start is called before the first frame update
    protected override void Start() 
    {
        //getcomponent = awake
        spellBook = GetComponent<SpellBook>();
        
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        Respawn();
        base.Update();
    }

   
    private void GetInput()
    {
        Direction = Vector2.zero;
  
        //input.getaxis(Horizontal i vertical)
        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            Direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            Direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            Direction += Vector2.right;
        }
        if (IsMoving)
        {
            StopAttack();
        }
        
    }
    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellIndex);
        
        IsAttacking = true;

        MyAnimator.SetBool("attack", IsAttacking);

        yield return new WaitForSeconds(newSpell.MyCastTime);

        if (currentTarget != null && InLightOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitpoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();

            s.Initialize(currentTarget, newSpell.MyDamage, transform);
        }
        StopAttack();
    }

    private void CastSpell(int spellIndex)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLightOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));

        }
    }
    private bool InLightOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);
            if (hit.collider == null)
            {
                return true;
            }
        }
        return false;
    }
    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }
        blocks[exitIndex].Activate();
    }


    public void StopAttack()
    {
        spellBook.StopCasting();

        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking);
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }

    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(tagCoin))
        {
            
            Destroy(other.gameObject);
        }
        else
        {
            if (other.gameObject.CompareTag(tagPotion))
            {
                health.MyCurrentValue += 20;
                Destroy(other.gameObject);
            }
        }
        if(other.gameObject.CompareTag(tagGems))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag(tagRange))
        {
            //TEST
            new WaitForSeconds(3);
            health.MyCurrentValue -= 5;
        }
        if (other.gameObject.CompareTag(tagSpeedPotion))
        {
            Speed += 0.1f;
            Destroy(other.gameObject);
        }
        // END TEST
        if (other.gameObject.CompareTag(tagTrueAnswer))
        {
            health.MyCurrentValue += 5;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag(tagFalseAnswer))
        {
            health.MyCurrentValue -= 15;
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
            Destroy(other.gameObject);

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag(tagRange))
        {
            health.MyCurrentValue -= 0;
        }
       
    }

    public void Respawn()
    {
        if (MyHealth.MyCurrentValue == 0 || MyHealth.MyCurrentValue <= 0)
        {
            MyHealth.MyCurrentValue = MyHealth.MyMaxValue;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        transform.position = new Vector2(destination.position.x, destination.position.y);
           
        }
    }
}
