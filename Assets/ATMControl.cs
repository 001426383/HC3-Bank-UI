using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ATMControl : MonoBehaviour, IPointerDownHandler {

	//All ATM screens
	public GameObject selectActionCanvas;
	public GameObject transferCanvas;

	public GameObject withdrawCanvas;
    public GameObject withdrawOverdraft;
    public GameObject withdrawConfirmation;

    public GameObject depositCanvas;
    public GameObject depositConfirmation;

    public GameObject introCanvas;
	public GameObject loginCanvas;

    //On screen text, which updates as users click buttons
    public GameObject wCTxt,dCTxt;

    public InputField depositAmt, transAmt;

    public Text withdrawAmntTxt, wAmt2, wCHQTxt, wSAVTxt, wVISATxt, wACC1Txt, wACC2Txt, wFromTxt, wNBTxt, wOBalance, wOFrom,
        dCHQTxt, dSAVTxt, dVISATxt, dACC1Txt, dACC2Txt, dToTxt, dAmntTxt, dNBTxt,
        fromCHQTxt, fromSAVTxt, fromVISATxt, fromACC1Txt, fromACC2Txt,
        toCHQTxt, toSAVTxt, toVISATxt, toACC1Txt, toACC2Txt;
        

    public Button wCHQ, wSAV, wVISA, wACC1, wACC2, wOProc, wOCanc, wProc, wCanc, wConfirm,
        dCHQ, dSAV, dVISA, dACC1, dACC2, dProc, dCanc, dConfirm;

    public static bool canWithDraw = false;
    public static bool canDeposit= false;
    //Numerical monetary values
    public static double CHQ = 1000.00;
    public static double SAV = 400.00;
    public static double VISA = -200.00;
    public static double ACCT1 = 10.00;
    public static double ACCT2 = 0.00;

	public static double withdrawAmnt, depositAmnt, transferAmnt, wTemp,dTemp = 0.00;
	public static string toAcct, fromAcct, withdrawAcct, depositAcct;
    public int wACC = 0;
    public int dACC = 0;
    public int fACC = 0;
    public int tACC = 0;
    public static Color click = new Color(23f/255f, 88f/255f, 154f/255f, 1f);

    //Startup Values
	void Start(){

        //Withdrawal Buttons
        wCHQ.onClick.AddListener(() => withDrawal(1));
        wSAV.onClick.AddListener(() => withDrawal(2));
        wVISA.onClick.AddListener(() => withDrawal(3));
        wACC1.onClick.AddListener(() => withDrawal(4));
        wACC2.onClick.AddListener(() => withDrawal(5));
        wConfirm.onClick.AddListener(withConfirm);
        wOProc.onClick.AddListener(withOProc);
        wOCanc.onClick.AddListener(withCancel);
        wProc.onClick.AddListener(withdraw);
        wCanc.onClick.AddListener(withCancel);
        // wReset.onClick.AddListener(() => withDrawal(6));

        // Deposit Buttons
        dCHQ.onClick.AddListener(() => dep(1));
        dSAV.onClick.AddListener(() => dep(2));
        dVISA.onClick.AddListener(() => dep(3));
        dACC1.onClick.AddListener(() => dep(4));
        dACC2.onClick.AddListener(() => dep(5));
        dConfirm.onClick.AddListener(depConfirm);
        dProc.onClick.AddListener(deposit);
        dCanc.onClick.AddListener(depCancel);
    }
    //7 withdrawal helper functions
    void withDrawal(int i)
    {
        canWithDraw = true;
        switch (i)
        {
            case 1:
                wACC = 1;
                wFromTxt.text = "Chequing";
                withReset();
                wCHQ.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                withReset();
                wFromTxt.text = "Savings";
                wSAV.GetComponent<Image>().color = Color.green;
                wACC = 2;
                break;
            case 3:
                withReset();
                wFromTxt.text = "VISA";
                wVISA.GetComponent<Image>().color = Color.green;
                wACC = 3;
                break;
            case 4:
                withReset();
                wFromTxt.text = "Account 1";
                wACC1.GetComponent<Image>().color = Color.green;
                wACC = 4;
                break;
            case 5:
                withReset();
                wFromTxt.text = "Account 2";
                wACC2.GetComponent<Image>().color = Color.green;
                wACC = 5;
                break;
            case 6:
                wACC = 0;
                break;
        }
    }
    void withReset()
    {
        wCHQ.GetComponent<Image>().color = click;
        wSAV.GetComponent<Image>().color = click;
        wVISA.GetComponent<Image>().color = click;
        wACC1.GetComponent<Image>().color = click;
        wACC2.GetComponent<Image>().color = click;
    }
    void dispWithdraw()
    {
        if (withdrawCanvas.activeInHierarchy == true)
        {
           
           switch (wACC)
            {
                case 0:
                    break;
                case 1:
                    wTemp = CHQ - withdrawAmnt;
                    wCHQTxt.text = "$" + wTemp.ToString();
                    if (wTemp < 0)
                    {
                        wCHQTxt.color = Color.red;
                    }
                    break;
                case 2:
                    wTemp = SAV - withdrawAmnt;
                    wSAVTxt.text = "$" + wTemp.ToString();
                    if (wTemp < 0)
                    {
                        wSAVTxt.color = Color.red;
                    }
                    break;
                case 3:
                    wTemp = VISA - withdrawAmnt;
                    wVISATxt.text = "$" + wTemp.ToString();
                    if (wTemp < 0)
                    {
                        wVISATxt.color = Color.red;
                    }
                    break;
                case 4:
                    wTemp = ACCT1 - withdrawAmnt;
                    wACC1Txt.text = "$" + wTemp.ToString();
                    if (wTemp < 0)
                    {
                        wACC1Txt.color = Color.red;
                    }
                    break;
                case 5:
                    wTemp = ACCT2 - withdrawAmnt;
                    wACC2Txt.text = "$" + wTemp.ToString();
                    if (wTemp < 0)
                    {
                        wACC2Txt.color = Color.red;
                    }
                    break;
            }
        }
    }
    void withConfirm()
    {
        if (wTemp >= 0)
        {
            wNBTxt.text = wTemp.ToString();
            wAmt2.text = withdrawAmnt.ToString();
            withdrawConfirmation.SetActive(true);
        }
        else
        {
            wOFrom.text = wFromTxt.text;
            wOBalance.text = "$" + wTemp.ToString();
            withdrawOverdraft.SetActive(true);
        }
    }
    void withCancel()
    {
        canWithDraw = false;
        wTemp = 0;
        wACC = 0;
        withReset();
        withdrawCanvas.SetActive(true);
        withdrawConfirmation.SetActive(false);
        withdrawOverdraft.SetActive(false);
    }
    void withOProc()
    {
        wNBTxt.text = wTemp.ToString();
        wAmt2.text = withdrawAmnt.ToString();
        withdrawConfirmation.SetActive(true);
    }
    void withdraw()
    {
        canWithDraw = false ;
        selectActionCanvas.SetActive(true);
        switch (wACC)
        {
            case 1:
                CHQ -= withdrawAmnt;
                break;
            case 2:
                SAV -= withdrawAmnt;
                break;
            case 3:
                VISA -= withdrawAmnt;
                break;
            case 4:
                ACCT1 -= withdrawAmnt;
                break;
            case 5:
                ACCT2 -= withdrawAmnt;
                break;
        }
        
        withReset();
        wFromTxt.text = "";
        wTemp = 0;
        wACC = 0;
        withdrawAmnt = 0;
        
    }
    
    //Deposit Helper Functions
    void dep(int i)
    {
        canDeposit = true;
        switch (i)
        {
            case 1:
                dACC = 1;
                dToTxt.text = "Chequing";
                depReset();
                dCHQ.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                depReset();
                dToTxt.text = "Savings";
                dSAV.GetComponent<Image>().color = Color.green;
                dACC = 2;
                break;
            case 3:
                depReset();
                dToTxt.text = "VISA";
                dVISA.GetComponent<Image>().color = Color.green;
                dACC = 3;
                break;
            case 4:
                depReset();
                dToTxt.text = "Account 1";
                dACC1.GetComponent<Image>().color = Color.green;
                dACC = 4;
                break;
            case 5:
                depReset();
                dToTxt.text = "Account 2";
                dACC2.GetComponent<Image>().color = Color.green;
                dACC = 5;
                break;
            case 6:
                dACC = 0;
                break;
        }
    }
    void depReset()
    {
        dCHQ.GetComponent<Image>().color = click;
        dSAV.GetComponent<Image>().color = click;
        dVISA.GetComponent<Image>().color = click;
        dACC1.GetComponent<Image>().color = click;
        dACC2.GetComponent<Image>().color = click;
    }
    void dispDeposit()
    {
        if (depositCanvas.activeInHierarchy == true)
        {

            switch (dACC)
            {
                case 0:
                    break;
                case 1:
                    dTemp = CHQ + withdrawAmnt;
                    dCHQTxt.text = "$" + dTemp.ToString();
                    if (wTemp < 0)
                    {
                        wCHQTxt.color = Color.red;
                    }
                    break;
                case 2:
                    dTemp = SAV + withdrawAmnt;
                    dSAVTxt.text = "$" + dTemp.ToString();
                    if (dTemp < 0)
                    {
                        dSAVTxt.color = Color.red;
                    }
                    break;
                case 3:
                    dTemp = VISA + withdrawAmnt;
                    dVISATxt.text = "$" + dTemp.ToString();
                    if (dTemp < 0)
                    {
                        dVISATxt.color = Color.red;
                    }
                    break;
                case 4:
                    dTemp = ACCT1 + withdrawAmnt;
                    wACC1Txt.text = "$" + dTemp.ToString();
                    if (dTemp < 0)
                    {
                        dACC1Txt.color = Color.red;
                    }
                    break;
                case 5:
                    dTemp = ACCT2 + withdrawAmnt;
                    dACC2Txt.text = "$" + dTemp.ToString();
                    if (dTemp < 0)
                    {
                        dACC2Txt.color = Color.red;
                    }
                    break;
            }
        }
    }
    void depConfirm()
    {
        dNBTxt.text = dTemp.ToString();
        dAmntTxt.text = depositAmnt.ToString();
        depositConfirmation.SetActive(true);
    }
    void depCancel()
    {
        canDeposit = false;
        dTemp = 0;
        dACC = 0;
        depReset();
        depositConfirmation.SetActive(false);
        depositCanvas.SetActive(true);
    }
    void deposit()
    {
        canDeposit = false;
        selectActionCanvas.SetActive(true);
        switch (wACC)
        {
            case 1:
                CHQ += depositAmnt;
                break;
            case 2:
                SAV += depositAmnt;
                break;
            case 3:
                VISA += depositAmnt;
                break;
            case 4:
                ACCT1 += depositAmnt;
                break;
            case 5:
                ACCT2 += depositAmnt;
                break;
        }

        withReset();
        wFromTxt.text = "";
        wTemp = 0;
        wACC = 0;
        withdrawAmnt = 0;
    }
    // Use this for initialization
    void OnEnable () {

		//Only one screen can stay active at a time
		if (this.gameObject.name == "SelectActionCanvas") {
			introCanvas.SetActive (false);
			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			loginCanvas.SetActive (false);
            withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);
            depositConfirmation.SetActive(false);
        } else if (this.gameObject.name == "TransferCanvas") {
            depositConfirmation.SetActive(false);
            selectActionCanvas.SetActive (false);
            withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);
            withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);

        } else if (this.gameObject.name == "WithdrawCanvas") {
            depositConfirmation.SetActive(false);
            transferCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);
            withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);
        } else if (this.gameObject.name == "DepositCanvas") {
            depositConfirmation.SetActive(false);
            transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);
            withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);
        }
      
		else if (this.gameObject.name == "IntroCanvas") {
            depositConfirmation.SetActive(false);
            transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			loginCanvas.SetActive (false);
            withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);
        } 
		else if (this.gameObject.name == "LoginCanvas") {
            depositConfirmation.SetActive(false);
            transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			introCanvas.SetActive (false);
            withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);
        }
        else if (this.gameObject.name == "wOverdraftCanvas")
        {
            depositConfirmation.SetActive(false);
            transferCanvas.SetActive(false);
            loginCanvas.SetActive(false);
            depositCanvas.SetActive(false);
            selectActionCanvas.SetActive(false);
            introCanvas.SetActive(false);
            //withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);
        }
        else if (this.gameObject.name == "wConfirmCanvas")
        {
            depositConfirmation.SetActive(false);
            transferCanvas.SetActive(false);
            loginCanvas.SetActive(false);
            depositCanvas.SetActive(false);
            selectActionCanvas.SetActive(false);
            introCanvas.SetActive(false);
            withdrawOverdraft.SetActive(false);
            //withdrawConfirmation.SetActive(false);
        }
        else if (this.gameObject.name == "dConfirmCanvas")
        {

            transferCanvas.SetActive(false);
            loginCanvas.SetActive(false);
            selectActionCanvas.SetActive(false);
            introCanvas.SetActive(false);
            withdrawOverdraft.SetActive(false);
            withdrawConfirmation.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		//Constantly update text displays
		if (this.gameObject.name == "WithdrawAmntTxt") {
			withdrawAmntTxt.text = withdrawAmnt.ToString();
		}

		if (this.gameObject.name == "CHQtxt") {
			fromCHQTxt.text = "$" + CHQ.ToString ();
			toCHQTxt.text ="$" +  CHQ.ToString ();
			if (CHQ < 0) {
				toCHQTxt.color = Color.red;
				fromCHQTxt.color = Color.red;
			} else {
				toCHQTxt.color = Color.green;
				fromCHQTxt.color = Color.green;
			}
		}
		else if (this.gameObject.name == "SAVtxt") {
			fromSAVTxt.text ="$" +  SAV.ToString ();
			toSAVTxt.text ="$" +  SAV.ToString ();
			if (SAV < 0) {
				fromSAVTxt.color = Color.red;
				toSAVTxt.color = Color.red;
			} else {
				fromSAVTxt.color = Color.green;
				toSAVTxt.color = Color.green;
			}
		}
		else if (this.gameObject.name == "VISAtxt") {
			fromVISATxt.text = "$" + VISA.ToString ();
			toVISATxt.text = "$" + VISA.ToString ();
			if (VISA < 0) {
				fromVISATxt.color = Color.red;
				toVISATxt.color = Color.red;
			} else {
				fromVISATxt.color = Color.green;
				toVISATxt.color = Color.green;
			}
		}
		else if (this.gameObject.name == "ACC1txt") {
			fromACC1Txt.text = "$" + ACCT1.ToString ();
			toACC1Txt.text = "$" + ACCT1.ToString ();
			if (ACCT1 < 0) {
				fromACC1Txt.color = Color.red;
				toACC1Txt.color = Color.red;
			} else {
				fromACC1Txt.color = Color.green;
				toACC1Txt.color = Color.green;
			}
		}
		else if (this.gameObject.name == "ACC2txt") {
			fromACC2Txt.text = "$" + ACCT2.ToString ();
			toACC2Txt.text = "$" + ACCT2.ToString ();
			if (ACCT2 < 0) {
				fromACC2Txt.color = Color.red;
				toACC2Txt.color = Color.red;
			} else {
				fromACC2Txt.color = Color.green;
				toACC2Txt.color = Color.green;
			}
		}
        if (withdrawAmnt < 0)
        {
            withdrawAmnt = 0;
        }
        if (depositAmnt < 0)
        {
            depositAmnt = 0;
        }
        if (depositAmnt > 10000)
        {
            depositAmnt = 10000f;
        }
        if (transferAmnt < 0)
        {
            transferAmnt = 0;
        }

        //withdraw & deposot/view screen stuff
        if (canWithDraw)
        {
            wConfirm.interactable = true;
            wCTxt.SetActive(true);
        }
        else
        {
            wCTxt.SetActive(false);
            wConfirm.interactable = false;
        }
        if (canDeposit)
        {
            dConfirm.interactable = true;
            dCTxt.SetActive(true);
        }
        else
        {
            dCTxt.SetActive(false);
            dConfirm.interactable = false;
        }
        wCHQTxt.text = "$" + CHQ.ToString();
        wSAVTxt.text = "$" + SAV.ToString();
        wVISATxt.text = "$" + VISA.ToString();
        wACC1Txt.text = "$" + ACCT1.ToString();
        wACC2Txt.text = "$" + ACCT2.ToString();

        dCHQTxt.text = "$" + CHQ.ToString();
        dSAVTxt.text = "$" + SAV.ToString();
        dVISATxt.text = "$" + VISA.ToString();
        dACC1Txt.text = "$" + ACCT1.ToString();
        dACC2Txt.text = "$" + ACCT2.ToString();

        if (CHQ < 0)
        {
            wCHQTxt.color = Color.red;
            dCHQTxt.color = Color.red;
        }
        else
        {
            wCHQTxt.color = Color.green;
            dCHQTxt.color = Color.green;
        }
        if (SAV < 0)
        {
            wSAVTxt.color = Color.red;
            dSAVTxt.color = Color.red;
        }
        else
        {
            wSAVTxt.color = Color.green;
            dSAVTxt.color = Color.green;
        }
        if (VISA < 0)
        {
            wVISATxt.color = Color.red;
            dVISATxt.color = Color.red;
        }
        else
        {
            wVISATxt.color = Color.green;
            dVISATxt.color = Color.green;
        }
        if (ACCT1 < 0)
        {
            wACC1Txt.color = Color.red;
            dACC1Txt.color = Color.red;
        }
        else
        {
            wACC1Txt.color = Color.green;
            dACC1Txt.color = Color.green;
        }
        if (ACCT2 < 0)
        {
            wACC2Txt.color = Color.red;
            dACC2Txt.color = Color.red;
        }
        else
        {
            wACC2Txt.color = Color.green;
            dACC2Txt.color = Color.green;
        }
        
        dispWithdraw();
        dispDeposit();

	}

	//Button input handlers - when a certain button is clicked, either activate a new screen, or update a numerical value
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log(this.gameObject.name + " Was Clicked.");

			//Return to home screen
			if (this.gameObject.name == "Home") {
				selectActionCanvas.SetActive (true);

			} 

			if (this.gameObject.name == "Logout") {
				introCanvas.SetActive (true);
			} 
			
			if (selectActionCanvas.activeInHierarchy == true) {

				if (this.gameObject.name == "Transfer") {
				
					transferCanvas.gameObject.SetActive (true);
				}

				if (this.gameObject.name == "Deposit") {

					depositCanvas.gameObject.SetActive (true);
				}

				if (this.gameObject.name == "Withdraw") {

					withdrawCanvas.gameObject.SetActive (true);
				}

			}
			else if (introCanvas.activeInHierarchy == true){
				if (this.gameObject.name == "IntroButton") {

					loginCanvas.gameObject.SetActive (true);
				}
			}
			else if (loginCanvas.activeInHierarchy == true){
				if (this.gameObject.name == "login" || this.gameObject.name == "login2" ) {

						selectActionCanvas.gameObject.SetActive (true);
					}
			}
			else if (withdrawCanvas.activeInHierarchy == true) {

				if (this.gameObject.name == "Plus20") {

					withdrawAmnt += 20;
				}

				if (this.gameObject.name == "Minus20") {

					withdrawAmnt -= 20;
				}

				if (this.gameObject.name == "Plus50") {

					withdrawAmnt += 50;
				}

				if (this.gameObject.name == "Minus50") {

					withdrawAmnt -= 50;
				}

				if (this.gameObject.name == "Plus100") {

					withdrawAmnt += 100;
				}

				if (this.gameObject.name == "Minus100") {

					withdrawAmnt -= 100;
				}

				if (this.gameObject.name == "Confirm") {

					
				}

			}
	}
}
