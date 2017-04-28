using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSequence : MonoBehaviour {

    public GameObject[] sequenceSlides;
    private float timeLimit = 20;
    private int currentIndex = 0;
    private bool inputCollectionPaused = false;
    private float timeElapsed = 0;

    void Start()
    {
        currentIndex = 0;
        HideAllSlides();
        DisplaySlide(0);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if((Input.anyKey && !inputCollectionPaused) || (timeElapsed >= timeLimit && !inputCollectionPaused))
        {
            if((currentIndex + 1) < sequenceSlides.Length)
            {
                HideAllSlides();
                currentIndex++;
                DisplaySlide(currentIndex);
                StartCoroutine(PauseInputCollection());
            }
            else if(currentIndex + 1 == sequenceSlides.Length)
            {
                SceneManager.LoadScene("MainScene");
            }
            timeElapsed = 0;
        }
    }

    private IEnumerator PauseInputCollection()
    {
        inputCollectionPaused = true;
        yield return new WaitForSeconds(0.25f);
        inputCollectionPaused = false;
    }

    private void HideAllSlides()
    {
        foreach(GameObject slide in sequenceSlides)
        {
            slide.gameObject.SetActive(false);
        }
    }

    private void DisplaySlide(int index)
    {
        if(index < sequenceSlides.Length)
        {
            GameObject slide = sequenceSlides[index];
            slide.gameObject.SetActive(true);
        }
    }
	
}
