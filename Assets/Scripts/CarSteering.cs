using System.Collections;
using UnityEngine;
using YG;

public class CarSteering : MonoBehaviour
{

	Rigidbody2D rb;

	[SerializeField]
	float controlCooldown = 2.0f, accelerationPower = 5f, steeringPower = 5f, engineSoundChangeSpeed = 1f;
	float steeringAmount, speed, direction;
	public AudioSource engineSound;
	public MobileController mobileController;
	private bool isControlFree = false;

	private void OnDisable()
	{
		engineSound.enabled = false;
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		engineSound = gameObject.GetComponent<AudioSource>();
		StartCoroutine(ControlCooldown());
	}

	private IEnumerator ControlCooldown()
	{
		yield return new WaitForSeconds(controlCooldown);
		isControlFree = true;
	}

	void FixedUpdate()
	{
		if (isControlFree)
		{


			if (mobileController.horizontal == 0)
			{
				steeringAmount = -Input.GetAxis("Horizontal");
			}
			else
			{
				steeringAmount = mobileController.horizontal;
			}
			if (mobileController.vertical == 0)
			{
				speed = Input.GetAxis("Vertical") * accelerationPower;
			}
			else
			{
				speed = mobileController.vertical * accelerationPower;
			}
			direction = Mathf.Sign(Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right)));
			rb.rotation += steeringAmount * steeringPower * rb.velocity.magnitude * direction;

			rb.AddRelativeForce(Vector2.right * speed);

			rb.AddRelativeForce(Vector2.up * rb.velocity.magnitude * steeringAmount / 2);

			if (YandexGame.savesData.isSoundOn)
			{

				if (speed != 0)
				{
					if (engineSound.pitch <= 2)
					{
						engineSound.pitch += engineSoundChangeSpeed * Time.fixedDeltaTime;
					}
				}
				else
				{
					if (engineSound.pitch >= 1)
					{
						engineSound.pitch -= engineSoundChangeSpeed * Time.fixedDeltaTime;
					}
				}

			}
		}
	}


	public void EngineSoundOnOff(bool isOn)
	{
		engineSound.enabled = isOn;
	}

}
