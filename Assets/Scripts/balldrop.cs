using UnityEngine;
using UnityEngine.UI; // Ensure this is included to use UnityEngine.UI.Text
using System.Collections;

public class PlaySoundOnDrop : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip dropSound;
    public UnityEngine.UI.Text counterText; // Specify the namespace explicitly
    public UnityEngine.UI.Text messageText; // Specify the namespace explicitly
    public float fadeDuration = 1f; // Duration for fading out

    private int ballCount = 0;

    private void Start()
    {
        // Ensure the message text is initially invisible
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (audioSource != null)
            {
                if (dropSound != null)
                {
                    audioSource.PlayOneShot(dropSound);
                    Destroy(other.gameObject, dropSound.length);
                }
                else
                {
                    audioSource.Play();
                    Destroy(other.gameObject, audioSource.clip.length);
                }
            }

            ballCount++;

            if (counterText != null)
            {
                counterText.text = "Balls in box : " + ballCount;
            }

            if (ballCount % 5 == 0)
            {
                StartCoroutine(ShowMessageFor10Seconds("Stop and take 10 seconds to breathe focus on the music"));
            }
        }
    }

    private IEnumerator ShowMessageFor10Seconds(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 1f); // Ensure it's fully opaque

            // Wait for 10 seconds
            yield return new WaitForSeconds(10f);

            // Fade out the message
            float elapsedTime = 0f;
            Color startColor = messageText.color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                messageText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }

            // Ensure the message is completely transparent and deactivate
            messageText.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
            messageText.gameObject.SetActive(false);
        }
    }
}
