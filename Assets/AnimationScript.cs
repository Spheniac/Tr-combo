using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private GameObject SlashSmear1;
    [SerializeField] private GameObject SlashSmear2;
    [SerializeField] private GameObject SlashSmear3;

    [SerializeField] private Transform SlashSpot;

    private bool slash1created;
    private bool slash2created;
    private bool slash3created;

    [SerializeField] private Animator animator;
    [SerializeField] private int combo;
    [SerializeField] private float combo_delay;
    [SerializeField] private float combo_resettime;
    [SerializeField] private float combo_time;
    [SerializeField] private bool isCombo;
    [SerializeField] private int actualCombo;
    [SerializeField] private string currentAnimation;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float slashforce1 = 10f;
    [SerializeField] private float slashforce2 = 10f;
    [SerializeField] private float slashforce3 = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (combo_resettime > 0) { combo_resettime -= 1 * Time.deltaTime; }
        else { isCombo = false; combo = 0; actualCombo = 0; }

        if (combo_delay > 0) { combo_delay -= 1 * Time.deltaTime; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            combo++;
            combo_resettime = 40f * Time.deltaTime;
            if (combo >= 3) { combo = 3; }
        }

        if ( combo > 0)
        {
            isCombo = true;
            combo_delay = 3f * Time.deltaTime;
            actualCombo += 1;
            animator.SetTrigger("Combo" + actualCombo.ToString());
            combo -= 1;
        }

        if (slash1created && currentAnimation != "atk_sword") { slash1created = false; }
        if (!slash1created && currentAnimation == "atk_sword")
        {
            GameObject _slashvfx=Instantiate(SlashSmear1, SlashSpot);
            rb.AddForce(Vector3.forward * slashforce1, ForceMode.Impulse);
            Destroy(_slashvfx, 1f);
            slash1created = true;
        }

        if (slash2created && currentAnimation != "atk_sword2") { slash2created = false; }
        if (!slash2created && currentAnimation == "atk_sword2")
        {
            GameObject _slashvfx2 = Instantiate(SlashSmear2, SlashSpot);
            rb.AddForce(Vector3.forward * slashforce2, ForceMode.Impulse);
            Destroy(_slashvfx2, 1f);
            slash2created = true;
        }

        if (slash3created && currentAnimation != "atk_sword3") { slash3created = false; combo_resettime = 0f;  }
        if (!slash3created && currentAnimation == "atk_sword3")
        {
            GameObject _slashvfx3 = Instantiate(SlashSmear3, SlashSpot);
            rb.AddForce(Vector3.forward * slashforce3, ForceMode.Impulse);
            Destroy(_slashvfx3, 1f);
            slash3created = true;
        }
    }
}