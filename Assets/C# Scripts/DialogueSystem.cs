using UnityEngine;
using System;
using TMPro;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private DialogueHolder orangeLight;
    [SerializeField] private DialogueHolder redLight;
    [SerializeField] private DialogueHolder startDialogue;
    [SerializeField] private DialogueHolder playerCheckedFriendBlog;
    [SerializeField] private DialogueHolder playerCheckedMyGram;
    [SerializeField] private DialogueHolder playerUsesPhoneAfterNotifications;
    [SerializeField] private DialogueHolder playerCheckedChripy;
    [SerializeField] private DialogueHolder playerUsedPhoneOnTheirOwn;
    [SerializeField] private DialogueHolder playerTookSelfie;
    [SerializeField] private DialogueHolder playerHasSendSelfie;
    [SerializeField] private DialogueHolder friendSendsMeme;
    [SerializeField] private DialogueHolder playerCheckedMessageFromFriendHost;

    [NonSerialized] public int state;

    private AudioSource[] allAudioSources;
    private TMP_Text dialogueText;
    private int randomLine;

    private int currentLineStart = 0;
    private int currentLineCheckedFriendBlog = 0;
    private int currentLineCheckedMyGram = 0;
    private int currentLineUsesPhoneAfterNotifications = 0;
    private int currentLineCheckedChripy = 0;
    private int currentLineUsedPhoneOnTheirOwn = 0;
    private int currentLineTookSelfie = 0;
    private int currentLineHasSendSelfie = 0;
    private int currentLineFriendSendsMeme = 0;
    private int currentLineCheckedMessageFromFriendHost = 0;

    private bool firstSentenceHasStartedStart = false;
    private bool firstSentenceHasStartedCheckedFriendBlog = false;
    private bool firstSentenceHasStartedCheckedMyGram = false;
    private bool firstSentenceHasStartedUPAN = false;
    private bool firstSentenceHasStartedCheckedChripy = false;
    private bool firstSentenceHasStartedUPOTO = false;
    private bool firstSentenceHasStartedTookSelfie = false;
    private bool firstSentenceHasStartedSendSelfie = false;
    private bool firstSentenceHasStartedFriendSendsMeme = false;
    private bool firstSentenceHasStartedCheckedMessageFromFriendHost = false;

    private bool State2DialogueHasFinished = false;
    private bool State3DialogueHasFinished = false;
    private bool State4DialogueHasFinished = false;
    private bool State5DialogueHasFinished = false;
    private bool State6DialogueHasFinished = false;
    private bool State7DialogueHasFinished = false;
    private bool State8DialogueHasFinished = false;
    private bool State9DialogueHasFinished = false;
    private bool State10DialogueHasFinished = false;
    private bool State11DialogueHasFinished = false;

    private bool timerHasStarted = false;
    private bool dialogueHasPlayed = false;
    private bool checkForContinueAfterTrafficLight = false;

    private GameManager gameManager;
    private PhoneHomeScreen phone;
    private ObjectiveHandler objective;
    private Instagram instagram;
    private Facebook facebook;
    private WhatsApp whatsapp;
    private CameraPhone camera;

    private void Awake()
    {
        dialogueText = GameObject.Find("/Canvas/DialogueText").GetComponent<TMP_Text>(); ;
        gameManager = FindObjectOfType<GameManager>();
        phone = FindObjectOfType<PhoneHomeScreen>();
        objective = FindObjectOfType<ObjectiveHandler>();
        instagram = FindObjectOfType<Instagram>();
        facebook = FindObjectOfType<Facebook>();
        whatsapp = FindObjectOfType<WhatsApp>();
        camera = FindObjectOfType<CameraPhone>();
    }

    private void Start()
    {
        if (dialogueText == null)
            throw new System.Exception("Add a dialogue text to the canvas.");
        if (gameManager == null)
            throw new System.Exception("Add a GameManager script to the scene.");
        if (phone == null)
            throw new System.Exception("Add a PhoneHomeScreen script to the scene.");
        if (objective == null)
            throw new System.Exception("Add a ObjectiveHandler script to the scene.");
        if (instagram == null)
            throw new System.Exception("Add a Instagram script to the scene.");
        if (facebook == null)
            throw new System.Exception("Add a Facebook script to the scene.");
        if (whatsapp == null)
            throw new System.Exception("Add a WhatsApp script to the scene.");
        if (camera == null)
            throw new System.Exception("Add a CameraPhone script to the scene.");

        // SET STATE BACK TO 2
        SetState(2);
    }

    private void Update()
    {
        if (gameManager.IsInputEnabled)
        {
            HandleDialogue();
            ContinueAfterTrafficLight();
        }
    }

    public void SetState(int pState)
    {
        state = pState;
    }

    public void StopDialogueAudio()
    {
        allAudioSources = this.gameObject.GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public void SetYellowLightState()
    {
        randomLine = UnityEngine.Random.Range(0, 2);

        StopDialogueAudio();
        orangeLight.timeForSentence[randomLine] = 3f;
        orangeLight.timeForSentence[randomLine] += Time.time;
        dialogueHasPlayed = false;
        state = 0;
    }

    public void SetRedLightState()
    {
        randomLine = UnityEngine.Random.Range(0, 2);

        StopDialogueAudio();
        redLight.timeForSentence[randomLine] = 4f;
        redLight.timeForSentence[randomLine] += Time.time;
        dialogueHasPlayed = false;
        state = 1;
    }

    private void HandleInteractions()
    {
        if (state == 2 && currentLineStart == startDialogue.dialogueText.Length - 1)
        {
            facebook.playerShouldOpenProfile = true;
            objective.SetObjective("Check FriendBlog notification to see Katy's cupcakes");
        }
        else if (state == 3 && currentLineCheckedFriendBlog == playerCheckedFriendBlog.dialogueText.Length - 1)
        {
            instagram.playerShouldOpenStory = true;
            objective.SetObjective("Look at Lilly's story on MyGram");
        }
        else if (state == 5 && currentLineUsesPhoneAfterNotifications == playerUsesPhoneAfterNotifications.dialogueText.Length - 1)
        {
            phone.playerShouldOpenChirpy = true;
            objective.SetObjective("Look for tour dates on Chirpy");
        }
        else if (state == 7 && currentLineUsedPhoneOnTheirOwn == playerUsedPhoneOnTheirOwn.dialogueText.Length - 1)
        {
            camera.playerShouldMakeSelfie = true;
            objective.SetObjective("Take a selfie with the camera");
        }
        else if (state == 8 && currentLineTookSelfie == playerTookSelfie.dialogueText.Length - 1)
        {
            whatsapp.playerShouldSendSelfie = true;
            objective.SetObjective("Send the selfie to Lauren via WhatsUp");
        }
        else if (state == 10 && currentLineFriendSendsMeme == friendSendsMeme.dialogueText.Length - 1)
        {
            instagram.playerShouldOpenMeme = true;
            objective.SetObjective("Check out the meme Lauren send you on MyGram");
        }
    }

    private void ContinueAfterTrafficLight()
    {
        if (checkForContinueAfterTrafficLight)
        {
            if (!State2DialogueHasFinished)
            {
                if (currentLineStart == 0 && !firstSentenceHasStartedStart)
                {
                    startDialogue.timeForSentence[currentLineStart] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 2;
                checkForContinueAfterTrafficLight = false;
                return;
            }
            else if (!State3DialogueHasFinished)
            {
                if (currentLineCheckedFriendBlog == 0 && !firstSentenceHasStartedCheckedFriendBlog)
                {
                    playerCheckedFriendBlog.timeForSentence[currentLineCheckedFriendBlog] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 3;
                checkForContinueAfterTrafficLight = false;
                return;
            }
            else if (!State4DialogueHasFinished)
            {
                if (currentLineCheckedMyGram == 0 && !firstSentenceHasStartedCheckedMyGram)
                {
                    playerCheckedMyGram.timeForSentence[currentLineCheckedMyGram] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 4;
                checkForContinueAfterTrafficLight = false;
                return;
            }
            else if (!State5DialogueHasFinished)
            {
                if (currentLineUsesPhoneAfterNotifications == 0 && !firstSentenceHasStartedUPAN)
                {
                    playerUsesPhoneAfterNotifications.timeForSentence[currentLineUsesPhoneAfterNotifications] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 5;
                checkForContinueAfterTrafficLight = false;
                return;
            }
            else if (!State6DialogueHasFinished)
            {
                if (currentLineCheckedChripy == 0 && !firstSentenceHasStartedCheckedChripy)
                {
                    playerUsesPhoneAfterNotifications.timeForSentence[currentLineUsesPhoneAfterNotifications] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 6;
                checkForContinueAfterTrafficLight = false;
                return;
            }
            else if (!State7DialogueHasFinished)
            {
                if (currentLineUsedPhoneOnTheirOwn == 0 && !firstSentenceHasStartedUPOTO)
                {
                    playerUsedPhoneOnTheirOwn.timeForSentence[currentLineUsedPhoneOnTheirOwn] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 7;
                return;
            }
            else if (!State8DialogueHasFinished)
            {
                if (currentLineTookSelfie == 0 && !firstSentenceHasStartedTookSelfie)
                {
                    playerTookSelfie.timeForSentence[currentLineTookSelfie] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 8;
                return;
            }
            else if (!State9DialogueHasFinished)
            {
                if (currentLineHasSendSelfie == 0 && !firstSentenceHasStartedSendSelfie)
                {
                    playerHasSendSelfie.timeForSentence[currentLineHasSendSelfie] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 9;
                return;
            }
            else if (!State10DialogueHasFinished)
            {
                if (currentLineFriendSendsMeme == 0 && !firstSentenceHasStartedFriendSendsMeme)
                {
                    friendSendsMeme.timeForSentence[currentLineFriendSendsMeme] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 10;
                return;
            }
            else if (!State11DialogueHasFinished)
            {
                if (currentLineCheckedMessageFromFriendHost == 0 && !firstSentenceHasStartedCheckedMessageFromFriendHost)
                {
                    playerCheckedMessageFromFriendHost.timeForSentence[currentLineCheckedMessageFromFriendHost] += Time.time;
                    dialogueHasPlayed = false;
                }

                state = 11;
                return;
            }
        }
    }

    private void HandleDialogue()
    {
        if (state == 0)
        {
            if (Time.time < orangeLight.timeForSentence[randomLine])
            {
                if (!dialogueHasPlayed)
                {
                    orangeLight.dialogueAudio[randomLine].Play();
                    dialogueText.text = orangeLight.dialogueText[randomLine];
                    dialogueHasPlayed = true;
                }
            }
            else
            {
                checkForContinueAfterTrafficLight = true;
                dialogueText.text = " ";
            }
        }

        else if (state == 1)
        {
            if (Time.time < redLight.timeForSentence[randomLine])
            {
                if (!dialogueHasPlayed)
                {
                    redLight.dialogueAudio[randomLine].Play();
                    dialogueText.text = redLight.dialogueText[randomLine];
                    dialogueHasPlayed = true;
                }
            }
            else
            {
                checkForContinueAfterTrafficLight = true;
                dialogueText.text = " ";
            }
        }

        else if (state == 2)
        {
            if (currentLineStart < startDialogue.dialogueText.Length)
            {
                if (Time.time < startDialogue.timeForSentence[currentLineStart])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedStart = true;
                        startDialogue.dialogueAudio[currentLineStart].Play();
                        dialogueText.text = startDialogue.dialogueText[currentLineStart];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineStart < startDialogue.dialogueText.Length - 1)
                {
                    currentLineStart++;
                    dialogueHasPlayed = false;
                    startDialogue.timeForSentence[currentLineStart] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (facebook.playerCheckedProfile)
            {
                State2DialogueHasFinished = true;
                playerCheckedFriendBlog.timeForSentence[currentLineCheckedFriendBlog] += Time.time;
                dialogueHasPlayed = false;
                StopDialogueAudio();
                state = 3;
            }
        }

        else if (state == 3)
        {
            if (currentLineCheckedFriendBlog < playerCheckedFriendBlog.dialogueText.Length)
            {
                if (Time.time < playerCheckedFriendBlog.timeForSentence[currentLineCheckedFriendBlog])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedCheckedFriendBlog = true;
                        playerCheckedFriendBlog.dialogueAudio[currentLineCheckedFriendBlog].Play();
                        dialogueText.text = playerCheckedFriendBlog.dialogueText[currentLineCheckedFriendBlog];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineCheckedFriendBlog < playerCheckedFriendBlog.dialogueText.Length - 1)
                {
                    currentLineCheckedFriendBlog++;
                    dialogueHasPlayed = false;
                    playerCheckedFriendBlog.timeForSentence[currentLineCheckedFriendBlog] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (instagram.playerCheckedStory)
            {
                State3DialogueHasFinished = true;
                playerCheckedMyGram.timeForSentence[currentLineCheckedMyGram] += Time.time;
                dialogueHasPlayed = false;
                StopDialogueAudio();
                state = 4;
            }
        }

        else if (state == 4)
        {
            if (currentLineCheckedMyGram < playerCheckedMyGram.dialogueText.Length)
            {
                if (Time.time < playerCheckedMyGram.timeForSentence[currentLineCheckedMyGram])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedCheckedMyGram = true;
                        playerCheckedMyGram.dialogueAudio[currentLineCheckedMyGram].Play();
                        dialogueText.text = playerCheckedMyGram.dialogueText[currentLineCheckedMyGram];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineCheckedMyGram < playerCheckedMyGram.dialogueText.Length - 1)
                {
                    currentLineCheckedMyGram++;
                    dialogueHasPlayed = false;
                    playerCheckedMyGram.timeForSentence[currentLineCheckedMyGram] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (currentLineCheckedMyGram == playerCheckedMyGram.dialogueText.Length - 1)
            {
                if (!timerHasStarted)
                {
                    StartCoroutine(WaitUntilNextState(10f));
                    timerHasStarted = true;
                }
            }
        }

        else if (state == 5)
        {
            if (currentLineUsesPhoneAfterNotifications < playerUsesPhoneAfterNotifications.dialogueText.Length)
            {
                if (Time.time < playerUsesPhoneAfterNotifications.timeForSentence[currentLineUsesPhoneAfterNotifications])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedUPAN = true;
                        playerUsesPhoneAfterNotifications.dialogueAudio[currentLineUsesPhoneAfterNotifications].Play();
                        dialogueText.text = playerUsesPhoneAfterNotifications.dialogueText[currentLineUsesPhoneAfterNotifications];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineUsesPhoneAfterNotifications < playerUsesPhoneAfterNotifications.dialogueText.Length - 1)
                {
                    currentLineUsesPhoneAfterNotifications++;
                    dialogueHasPlayed = false;
                    playerUsesPhoneAfterNotifications.timeForSentence[currentLineUsesPhoneAfterNotifications] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (phone.playerOpenendChirpy)
            {
                State5DialogueHasFinished = true;
                playerCheckedChripy.timeForSentence[currentLineCheckedChripy] += Time.time;
                dialogueHasPlayed = false;
                StopDialogueAudio();
                state = 6;
            }
        }

        else if (state == 6)
        {
            if (currentLineCheckedChripy < playerCheckedChripy.dialogueText.Length)
            {
                if (Time.time < playerCheckedChripy.timeForSentence[currentLineCheckedChripy])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedCheckedChripy = true;
                        playerCheckedChripy.dialogueAudio[currentLineCheckedChripy].Play();
                        dialogueText.text = playerCheckedChripy.dialogueText[currentLineCheckedChripy];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineCheckedChripy < playerCheckedChripy.dialogueText.Length - 1)
                {
                    currentLineCheckedChripy++;
                    dialogueHasPlayed = false;
                    playerCheckedChripy.timeForSentence[currentLineCheckedChripy] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (currentLineCheckedChripy == playerCheckedChripy.dialogueText.Length - 1)
            {
                if (!timerHasStarted)
                {
                    StartCoroutine(WaitUntilNextState(10f));
                    timerHasStarted = true;
                }
            }
        }

        else if (state == 7)
        {
            if (currentLineUsedPhoneOnTheirOwn < playerUsedPhoneOnTheirOwn.dialogueText.Length)
            {
                if (Time.time < playerUsedPhoneOnTheirOwn.timeForSentence[currentLineUsedPhoneOnTheirOwn])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedUPOTO = true;
                        playerUsedPhoneOnTheirOwn.dialogueAudio[currentLineUsedPhoneOnTheirOwn].Play();
                        dialogueText.text = playerUsedPhoneOnTheirOwn.dialogueText[currentLineUsedPhoneOnTheirOwn];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineUsedPhoneOnTheirOwn < playerUsedPhoneOnTheirOwn.dialogueText.Length - 1)
                {
                    currentLineUsedPhoneOnTheirOwn++;
                    dialogueHasPlayed = false;
                    playerUsedPhoneOnTheirOwn.timeForSentence[currentLineUsedPhoneOnTheirOwn] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (camera.playerMadeSelfie)
            {
                State7DialogueHasFinished = true;
                playerTookSelfie.timeForSentence[currentLineTookSelfie] += Time.time;
                dialogueHasPlayed = false;
                StopDialogueAudio();
                state = 8;
            }
        }

        else if (state == 8)
        {
            if (currentLineTookSelfie < playerTookSelfie.dialogueText.Length)
            {
                if (Time.time < playerTookSelfie.timeForSentence[currentLineTookSelfie])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedTookSelfie = true;
                        playerTookSelfie.dialogueAudio[currentLineTookSelfie].Play();
                        dialogueText.text = playerTookSelfie.dialogueText[currentLineTookSelfie];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineTookSelfie < playerTookSelfie.dialogueText.Length - 1)
                {
                    currentLineTookSelfie++;
                    dialogueHasPlayed = false;
                    playerTookSelfie.timeForSentence[currentLineTookSelfie] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (whatsapp.playerHasSendSelfie)
            {
                State8DialogueHasFinished = true;
                playerHasSendSelfie.timeForSentence[currentLineHasSendSelfie] += Time.time;
                dialogueHasPlayed = false;
                StopDialogueAudio();
                state = 9;
            }
        }

        else if (state == 9)
        {
            if (currentLineHasSendSelfie < playerHasSendSelfie.dialogueText.Length)
            {
                if (Time.time < playerHasSendSelfie.timeForSentence[currentLineHasSendSelfie])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedSendSelfie = true;
                        playerHasSendSelfie.dialogueAudio[currentLineHasSendSelfie].Play();
                        dialogueText.text = playerHasSendSelfie.dialogueText[currentLineHasSendSelfie];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineHasSendSelfie < playerHasSendSelfie.dialogueText.Length - 1)
                {
                    currentLineHasSendSelfie++;
                    dialogueHasPlayed = false;
                    playerHasSendSelfie.timeForSentence[currentLineHasSendSelfie] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (currentLineHasSendSelfie == playerHasSendSelfie.dialogueText.Length - 1)
            {
                if (!timerHasStarted)
                {
                    StartCoroutine(WaitUntilNextState(10f));
                    timerHasStarted = true;
                }
            }
        }

        else if (state == 10)
        {
            if (currentLineFriendSendsMeme < friendSendsMeme.dialogueText.Length)
            {
                if (Time.time < friendSendsMeme.timeForSentence[currentLineFriendSendsMeme])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedFriendSendsMeme = true;
                        friendSendsMeme.dialogueAudio[currentLineFriendSendsMeme].Play();
                        dialogueText.text = friendSendsMeme.dialogueText[currentLineFriendSendsMeme];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineFriendSendsMeme < friendSendsMeme.dialogueText.Length - 1)
                {
                    currentLineFriendSendsMeme++;
                    dialogueHasPlayed = false;
                    friendSendsMeme.timeForSentence[currentLineFriendSendsMeme] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }

            if (instagram.playerCheckedMeme)
            {
                if (!timerHasStarted)
                {
                    StartCoroutine(WaitUntilNextState(10f));
                    timerHasStarted = true;
                }
            }
        }

        else if (state == 11)
        {
            if (currentLineCheckedMessageFromFriendHost < playerCheckedMessageFromFriendHost.dialogueText.Length)
            {
                if (Time.time < playerCheckedMessageFromFriendHost.timeForSentence[currentLineCheckedMessageFromFriendHost])
                {
                    if (!dialogueHasPlayed)
                    {
                        firstSentenceHasStartedCheckedMessageFromFriendHost = true;
                        playerCheckedMessageFromFriendHost.dialogueAudio[currentLineCheckedMessageFromFriendHost].Play();
                        dialogueText.text = playerCheckedMessageFromFriendHost.dialogueText[currentLineCheckedMessageFromFriendHost];
                        dialogueHasPlayed = true;
                    }
                    HandleInteractions();
                }
                else if (currentLineCheckedMessageFromFriendHost < playerCheckedMessageFromFriendHost.dialogueText.Length - 1)
                {
                    currentLineCheckedMessageFromFriendHost++;
                    dialogueHasPlayed = false;
                    playerCheckedMessageFromFriendHost.timeForSentence[currentLineCheckedMessageFromFriendHost] += Time.time;
                }
                else
                    dialogueText.text = " ";
            }
            else
                State11DialogueHasFinished = true;
        }
    }

    private IEnumerator WaitUntilNextState(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (state == 4 || state == 0 || state == 1)
        {
            State4DialogueHasFinished = true;
            playerUsesPhoneAfterNotifications.timeForSentence[currentLineUsesPhoneAfterNotifications] += Time.time;
            dialogueHasPlayed = false;
            StopDialogueAudio();
            state = 5;
            timerHasStarted = false;
        }
        else if (state == 6 || state == 0 || state == 1)
        {
            State6DialogueHasFinished = true;
            playerUsedPhoneOnTheirOwn.timeForSentence[currentLineUsedPhoneOnTheirOwn] += Time.time;
            dialogueHasPlayed = false;
            StopDialogueAudio();
            state = 7;
            timerHasStarted = false;
        }
        else if (state == 9 || state == 0 || state == 1)
        {
            State9DialogueHasFinished = true;
            friendSendsMeme.timeForSentence[currentLineFriendSendsMeme] += Time.time;
            dialogueHasPlayed = false;
            StopDialogueAudio();
            state = 10;
            timerHasStarted = false;
        }
        else if (state == 10 || state == 0 || state == 1)
        {
            State10DialogueHasFinished = true;
            playerCheckedMessageFromFriendHost.timeForSentence[currentLineCheckedMessageFromFriendHost] += Time.time;
            dialogueHasPlayed = false;
            StopDialogueAudio();
            state = 11;
            timerHasStarted = false;
        }
    }
}
