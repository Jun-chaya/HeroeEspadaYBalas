using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PSVita;

public class MovimientoTopDown : Singleton<MovimientoTopDown>
{

	[SerializeField] private float velocidadMovimiento = 3f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Vector2 direccion;
	[SerializeField] private Transform weaponCollider;


	private Knockback knockback;
    private float startingMoveSpeed;
    private bool isDashing = false;
	public Animator animator;
	private Rigidbody2D rb2D;

    protected override void Awake() {
		base.Awake();
		rb2D = GetComponent<Rigidbody2D>();
		knockback = GetComponent<Knockback>();
		
	}
    private void Start()
    {
      

        startingMoveSpeed = velocidadMovimiento;

        ActiveInventory.Instance.EquipStartingWeapon();

    }


    private void OnDisable()
    {
        MovimientoTopDown.Destroy(this);
    }

    void Update () {
#if UNITY_EDITOR
		direccion = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
		animator.SetFloat("Horizontal",direccion.x);
		animator.SetFloat("Vertical",direccion.y);
		animator.SetFloat("Speed",direccion.sqrMagnitude);
        if (Input.GetButtonDown("Jump"))
        {
            Dash();
        }

#else

        direccion = new Vector2(UnityEngine.Input.GetAxis("LeftStickH"),
		UnityEngine.Input.GetAxis("LeftStickV")) ;
        animator.SetFloat("Horizontal", direccion.x);
        animator.SetFloat("Vertical", direccion.y);
        animator.SetFloat("Speed", direccion.sqrMagnitude);
		if (Input.GetKeyDown(KeyCode.Joystick1Button5))
		{
			Dash();
		}

#endif
    }

    private void FixedUpdate ()
	{
		if (knockback.GettingKnockedBack || PlayerHealth.Instance.isDead)
		{
			return;
		}
		rb2D.MovePosition(rb2D.position + direccion * (velocidadMovimiento * Time.fixedDeltaTime));
		
	}

	public Transform getWeaponCollider()
	{
		return weaponCollider;
	}
	private void Dash()
	{
		if (!isDashing && Stamina.Instance.CurrentStamina > 0) {
            Stamina.Instance.UseStamina();

            isDashing = true;
        velocidadMovimiento *= dashSpeed;
		myTrailRenderer.emitting = true;
		StartCoroutine(EndDashRoutine());
        }
    }

	private IEnumerator EndDashRoutine()
	{
		float dashTime = .2f;
		float dashCD = .25f;
		yield return new WaitForSeconds(dashTime);
        velocidadMovimiento = startingMoveSpeed;
		myTrailRenderer.emitting=false;
        yield return new WaitForSeconds(dashCD);
		isDashing=false;
    }

}
