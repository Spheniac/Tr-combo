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
    [SerializeField] private string currentAnimation;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float slashforce1 = 10f;
    [SerializeField] private float slashforce2 = 10f;
    [SerializeField] private float slashforce3 = 10f;

    [SerializeField] private int combo;
    [SerializeField] private float combo_time;
    [SerializeField] private float combo_timereset = 1f;
    [SerializeField] private bool isCombo;
    [SerializeField] private bool canCombo = true;
    [SerializeField] private float comboWindowStart = 0.6f; // % de combo_timereset donde se abre la ventana
    private string[] comboAnims = { "atk_sword", "atk_sword2", "atk_sword3" };
    private bool bufferedInput = false;

    void Update()
    {
        /// ESPADA
        if (isCombo)
        {
            combo_time += Time.deltaTime;

            // Ventana de combo basada en tu propio timer, ya no depende del nombre del state del Animator
            if (bufferedInput && combo_time >= combo_timereset * comboWindowStart)
            {
                combo++;
                bufferedInput = false;
                animator.Play(comboAnims[combo - 1]);
                combo_time = 0;
            }

            if (combo_time >= combo_timereset)
            {
                combo = 0;
                combo_time = 0;
                isCombo = false;
                canCombo = true;
                bufferedInput = false;
            }

            if (combo > 3)
            {
                combo = 3;
                canCombo = false;
                combo_timereset = 0.2f;
            }
        }

        currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (combo == 0)
            {
                isCombo = true;
                combo = 1;
                animator.Play(comboAnims[0]);
                combo_time = 0;
            }
            else if (canCombo && combo < 3)
            {
                bufferedInput = true;
            }
        }

        if (slash1created && currentAnimation != "atk_sword") { slash1created = false; }
        if (!slash1created && currentAnimation == "atk_sword")
        {
            GameObject _slashvfx = Instantiate(SlashSmear1, SlashSpot);
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

        if (slash3created && currentAnimation != "atk_sword3") { slash3created = false; }
        if (!slash3created && currentAnimation == "atk_sword3")
        {
            GameObject _slashvfx3 = Instantiate(SlashSmear3, SlashSpot);
            rb.AddForce(Vector3.forward * slashforce3, ForceMode.Impulse);
            Destroy(_slashvfx3, 1f);
            slash3created = true;
        }
    }
}