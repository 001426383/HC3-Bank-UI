using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ATMControl : MonoBehaviour, IPointerDownHandler {

	//All ATM screens
	public GameObject selectActionCanvas;

	public GameObject transferCanvas;
    public GameObject transferOverdraft;
    public GameObject transferConfirmation;

    public GameObject withdrawCanvas;
    public GameObject withdrawOverdraft;
    public GameObject withdrawConfirmation;

    public GameObject depositCanvas;
    public GameObject depositConfirmation;

    public GameObject introCanvas;
	public GameObject loginCanvas;

    //On screen text, which updates as users click buttons
    public GameObject wCTxt,dCTxt,tCTxt;

    public InputField depositAmt, transAmt;

    public Text withdrawAmntTxt, wAmt2, wCHQTxt, wSAVTxt, wVISATxt, wACC1Txt, wACC2Txt, wFromTxt, wNBTxt, wOBalance, wOFrom,
        dCHQTxt, dSAVTxt, dVISATxt, dACC1Txt, dACC2Txt, dToTxt, dAmntTxt, dNBTxt,
        fCHQTxt, fSAVTxt, fVISATxt, fACC1Txt, fACC2Txt,
        tCHQTxt, tSAVTxt, tVISATxt, tACC1Txt, tACC2Txt,
        tFromTxt, tToTxt, tOBalanceTxt, tOAccTxt, tFBalanceTxt, tTBalanceTxt;


    public Button wCHQ, wSAV, wVISA, wACC1, wACC2, wOProc, wOCanc, wProc, wCanc, wConfirm,
        dCHQ, dSAV, dVISA, dACC1, dACC2, dProc, dCanc, dConfirm,
        fCHQ, fSAV, fVISA, fACC1, fACC2,
        tCHQ, tSAV, tVISA, tACC1, tACC2,
        tProc, tCanc, tOProc, tOCanc, tConfirm;

    public static bool canWithDraw = false;
    public static bool canDeposit= false;
    public static bool pickedFrom = false;
    public static bool pickedTo = false;

    //Numerical monetary values
    public static double CHQ = 1000.00;
    public static double SAV = 400.00;
    public static double VISA = -200.00;
    public static double ACCT1 = 10.00;
    public static double ACCT2 = 0.00;

	public static double withdrawAmnt, depositAmnt, transferAmnt, wTemp,dTemp,fTemp,tTemp = 0.00;
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

        //Transfer Buttons
        fCHQ.onClick.AddListener(() => transFrom(1));
        fSAV.onClick.AddListener(() => transFrom(2));
        fVISA.onClick.AddListener(() => transFrom(3));
        fACC1.onClick.AddListener(() => transFrom(4));
        fACC2.onClick.AddListener(() => transFrom(5));
        tCHQ.onClick.AddListener(() => transTo(1));
        tSAV.onClick.AddListener(() => transTo(2));
        tVISA.onClick.AddListener(() => transTo(3));
        tACC1.onClick.AddListener(() => transTo(4));
        tACC2.onClick.AddListener(() => transTo(5));

        tConfirm.onClick.AddListener(transConfirm);
        tOProc.onClick.AddListener(transOProc);
        tOCanc.onClick.AddListener(transCancel);
        tProc.onClick.AddListener(transfer);
        tCanc.onClick.AddListener(transCancel);
    }

    //Transfer helper Functions
    void transFrom(int i)
    {
        pickedFrom = true;
        switch (i)
        {
            case 1:
                fACC = 1;
                tFromTxt.text = "Chequing";
                transFromReset();
                fCHQ.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                fACC = 2;
                transFromReset();
                tFromTxt.text = "Savings";
                fSAV.GetComponent<Image>().color = Color.green;
                
                break;
            case 3:
                fACC = 3;
                transFromReset();
                tFromTxt.text = "VISA";
                fVISA.GetComponent<Image>().color = Color.green;
                
                break;
            case 4:
                fACC = 4;
                transFromReset();
                tFromTxt.text = "Account 1";
                fACC1.GetComponent<Image>().color = Color.green;
                
                break;
            case 5:
                fACC = 5;
                transFromReset();
                tFromTxt.text = "Account 2";
                fACC2.GetComponent<Image>().color = Color.green;
                
                break;
            case 6:
                fACC = 0;
                break;
        }
    }
    void transTo(int i)
    {
        pickedTo = true;
        switch (i)
        {
            case 1:
                tACC = 1;
                tToTxt.text = "Chequing";
                transToReset();
                tCHQ.GetComponent<Image>().color = Color.green;
                break;
            case 2:
                transToReset();
                tToTxt.text = "Savings";
                tSAV.GetComponent<Image>().color = Color.green;
                tACC = 2;
                break;
            case 3:
                transToReset();
                tToTxt.text = "VISA";
                tVISA.GetComponent<Image>().color = Color.green;
                tACC = 3;
                break;
            case 4:
                transToReset();
                tToTxt.text = "Account 1";
                tACC1.GetComponent<Image>().color = Color.green;
                tACC = 4;
                break;
            case 5:
                transToReset();
                tToTxt.text = "Account 2";
                tACC2.GetComponent<Image>().color = Color.green;
                tACC = 5;
                break;
            case 6:
                tACC = 0;
                break;
        }
    }
    void transFromDisp()
    {
        if (transferCanvas.activeInHierarchy == true)
        {
            if (double.TryParse(transAmt.text, out transferAmnt))
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(transAmt.text) && transAmt.text != " ")
                {
                    transAmt.text = "0.00";
                    transferAmnt = 0.00f;
                }
                else
                {
                    transferAmnt = 0.00f;
                }
            }
            if (transferAmnt < 0)
            {
                transferAmnt *= -1;
                transAmt.text = transferAmnt.ToString();
            }
            Debug.Log(transferAmnt.ToString());
            switch (fACC)
            {
                case 0:
                    break;
                case 1:
                    fTemp = CHQ - transferAmnt;
                    fCHQTxt.text = "$" + fTemp.ToString();
                    if (fTemp < 0)
                    {
                        fCHQTxt.color = Color.red;
                    }
                    break;
                case 2:
                    fTemp = SAV - transferAmnt;
                    fSAVTxt.text = "$" + fTemp.ToString();
                    if (fTemp < 0)
                    {
                        fSAVTxt.color = Color.red;
                    }
                    break;
                case 3:
                    fTemp = VISA - transferAmnt;
                    fVISATxt.text = "$" + fTemp.ToString();
                    if (fTemp < 0)
                    {
                        fVISATxt.color = Color.red;
                    }
                    break;
                case 4:
                    fTemp = ACCT1 - transferAmnt;
                    fACC1Txt.text = "$" + fTemp.ToString();
                    if (fTemp < 0)
                    {
                        fACC1Txt.color = Color.red;
                    }
                    break;
                case 5:
                    fTemp = ACCT2 - transferAmnt;
                    fACC2Txt.text = "$" + fTemp.ToString();
                    if (fTemp < 0)
                    {
                        fACC2Txt.color = Color.red;
                    }
                    break;
            }
        }
    }
    void transToDisp()
    {
        if (transferCanvas.activeInHierarchy == true)
        {
            switch (tACC)
            {
                case 0:
                    break;
                case 1:
                    tTemp = CHQ + transferAmnt;
                    tCHQTxt.text = "$" + tTemp.ToString();
                    if (tTemp > 0)
                    {
                        tCHQTxt.color = Color.green;
                    }
                    break;
                case 2:
                    tTemp = SAV + transferAmnt;
                    tSAVTxt.text = "$" + tTemp.ToString();
                    if (tTemp > 0)
                    {
                        tSAVTxt.color = Color.green;
                    }
                    break;
                case 3:
                    tTemp = VISA + transferAmnt;
                    tVISATxt.text = "$" + tTemp.ToString();
                    if (tTemp > 0)
                    {
                        tVISATxt.color = Color.green;
                    }
                    break;
                case 4:
                    tTemp = ACCT1 + transferAmnt;
                    tACC1Txt.text = "$" + tTemp.ToString();
                    if (tTemp > 0)
                    {
                        tACC1Txt.color = Color.green;
                    }
                    break;
                case 5:
                    tTemp = ACCT2 + transferAmnt;
                    tACC2Txt.text = "$" + tTemp.ToString();
                    if (tTemp > 0)
                    {
                        tACC2Txt.color = Color.green;
                    }
                    break;
            }
        }
    }
    void transConfirm()
    {
        
   
            tFBalanceTxt.text = "$" + fTemp.ToString();
            tTBalanceTxt.text = "$" + tTemp.ToString();
            //wAmt2.text = withdrawAmnt.ToString();
            transferConfirmation.SetActive(true);
        
    }
    void transOProc()
    {
        tTBalanceTxt.text = tTemp.ToString();
        tFBalanceTxt.text = fTemp.ToString();

    }
    void transCancel()
    {
        pickedFrom = false;
        pickedTo = false;
        fTemp = 0;
        tTemp = 0;
        transAmt.text = "0.00";
        transFromReset();
        transToReset();
        transferConfirmation.SetActive(false);
        transferCanvas.SetActive(true);
    }
    void transFromReset()
    {
        fCHQ.GetComponent<Image>().color = click;
        fSAV.GetComponent<Image>().color = click;
        fVISA.GetComponent<Image>().color = click;
        fACC1.GetComponent<Image>().color = click;
        fACC2.GetComponent<Image>().color = click;
    }
    void transToReset()
    {
        tCHQ.GetComponent<Image>().color = click;
        tSAV.GetComponent<Image>().color = click;
        tVISA.GetComponent<Image>().color = click;
        tACC1.GetComponent<Image>().color = click;
        tACC2.GetComponent<Image>().color = click;

       
        //tCHQ.interactable = true;
        //tSAV.interactable = true;
        //tVISA.interactable = true;
        //tACC1.interactable = true;
        //tACC2.interactable = true;
    }
    void transfer()
    {
        pickedFrom = false;
        switch (fACC)
        {
            case 1:
                CHQ -= transferAmnt;
                //Debug.Log(CHQ.ToString());
                break;
            case 2:
                SAV -= transferAmnt;
               // Debug.Log(SAV.ToString());
                break;
            case 3:
                VISA -= transferAmnt;
                //Debug.Log(VISA.ToString());
                break;
            case 4:
                ACCT1 -= transferAmnt;
                //Debug.Log(ACCT1.ToString());
                break;
            case 5:
                ACCT2 -= transferAmnt;
               // Debug.Log(ACCT2.ToString());
                break;
        }
        switch (tACC)
        {
            case 1:
                CHQ += transferAmnt;
              //  Debug.Log(CHQ.ToString());
                break;
            case 2:
                SAV += transferAmnt;
                //Debug.Log(SAV.ToString());
                break;
            case 3:
                VISA += transferAmnt;
                //Debug.Log(VISA.ToString());
                break;
            case 4:
                ACCT1 += transferAmnt;
                //Debug.Log(ACCT1.ToString());
                break;
            case 5:
                ACCT2 += transferAmnt;
                //Debug.Log(ACCT2.ToString());
                break;
        }
        pickedTo = false;
        transAmt.text = "0.00";
        transferAmnt = 0.00;
        transFromReset();
        transToReset();
        dToTxt.text = "";
        dTemp = 0;
        dACC = 0;
        selectActionCanvas.SetActive(true);
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
            if (double.TryParse(depositAmt.text, out depositAmnt))
            {

            }
            else
            {
                if (!string.IsNullOrEmpty(depositAmt.text) && depositAmt.text != " ")
                {
                    depositAmt.text = "0.00";
                    depositAmnt = 0.00f;
                }
                else
                {
                    depositAmnt = 0.00f;
                }
            }
            if (depositAmnt < 0)
            {
                depositAmnt *= -1;
                depositAmt.text = depositAmnt.ToString();
            }
            switch (dACC)
            {
                case 0:
                    break;
                case 1:
                    dTemp = CHQ + depositAmnt;
                    dCHQTxt.text = "$" + dTemp.ToString();
                    if (dTemp > 0)
                    {
                        dCHQTxt.color = Color.green;
                    }
                    break;
                case 2:
                    dTemp = SAV + depositAmnt;
                    dSAVTxt.text = "$" + dTemp.ToString();
                    if (dTemp > 0)
                    {
                        dSAVTxt.color = Color.green;
                    }
                    break;
                case 3:
                    dTemp = VISA + depositAmnt;
                    dVISATxt.text = "$" + dTemp.ToString();
                    if (dTemp > 0)
                    {
                        dVISATxt.color = Color.green;
                    }
                    break;
                case 4:
                    dTemp = ACCT1 + depositAmnt;
                    dACC1Txt.text = "$" + dTemp.ToString();
                    if (dTemp > 0)
                    {
                        dACC1Txt.color = Color.green;
                    }
                    break;
                case 5:
                    dTemp = ACCT2 + depositAmnt;
                    dACC2Txt.text = "$" + dTemp.ToString();
                    if (dTemp > 0)
                    {
                        dACC2Txt.color = Color.green;
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
        depositAmt.text = "0.00";
        dACC = 0;
        depReset();
        depositConfirmation.SetActive(false);
        depositCanvas.SetActive(true);
    }
    void deposit()
    {
        Debug.Log(depositAmnt.ToString());
        canDeposit = false;
        switch (dACC)
        {
            case 1:
                CHQ += depositAmnt;
                Debug.Log(CHQ.ToString());
                break;
            case 2:
                SAV += depositAmnt;
                Debug.Log(SAV.ToString());
                break;
            case 3:
                VISA += depositAmnt;
                Debug.Log(VISA.ToString());
                break;
            case 4:
                ACCT1 += depositAmnt;
                Debug.Log(ACCT1.ToString());
                break;
            case 5:
                ACCT2 += depositAmnt;
                Debug.Log(ACCT2.ToString());
                break;
        }
        depositAmt.text = "0.00";
        depositAmnt = 0.00;
        depReset();
        dToTxt.text = "";
        dTemp = 0;
        dACC = 0;
        selectActionCanvas.SetActive(true);
    }
    //Logout Fxn
    void logout()
    {
        withCancel();
        depCancel();
        transCancel();
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
            transferConfirmation.SetActive(false);
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
        else if (this.gameObject.name == "TransferConfirmCanvas")
        {
            depositCanvas.SetActive(false);
            depositConfirmation.SetActive(false);
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
        if (transferAmnt > 10000)
        {
            transferAmnt = 10000;
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
        if (pickedFrom && pickedTo)
        {
            transAmt.interactable = true;
            tConfirm.interactable = true;
            tCTxt.SetActive(true);
        }
        else
        {
            transAmt.interactable = true;
            tCTxt.SetActive(false);
            tConfirm.interactable = false;
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

        fCHQTxt.text = "$" + CHQ.ToString();
        fSAVTxt.text = "$" + SAV.ToString();
        fVISATxt.text = "$" + VISA.ToString();
        fACC1Txt.text = "$" + ACCT1.ToString();
        fACC2Txt.text = "$" + ACCT2.ToString();

        tCHQTxt.text = "$" + CHQ.ToString();
        tSAVTxt.text = "$" + SAV.ToString();
        tVISATxt.text = "$" + VISA.ToString();
        tACC1Txt.text = "$" + ACCT1.ToString();
        tACC2Txt.text = "$" + ACCT2.ToString();

        if (CHQ < 0)
        {
            wCHQTxt.color = Color.red;
            dCHQTxt.color = Color.red;
            fCHQTxt.color = Color.red;
            tCHQTxt.color = Color.red;
        }
        else
        {
            wCHQTxt.color = Color.green;
            dCHQTxt.color = Color.green;
            fCHQTxt.color = Color.green;
            tCHQTxt.color = Color.green;
        }
        if (SAV < 0)
        {
            wSAVTxt.color = Color.red;
            dSAVTxt.color = Color.red;
            fSAVTxt.color = Color.red;
            tSAVTxt.color = Color.red;
        }
        else
        {
            wSAVTxt.color = Color.green;
            dSAVTxt.color = Color.green;
            fSAVTxt.color = Color.green;
            tSAVTxt.color = Color.green;
        }
        if (VISA < 0)
        {
            wVISATxt.color = Color.red;
            dVISATxt.color = Color.red;
            fVISATxt.color = Color.red;
            tVISATxt.color = Color.red;
        }
        else
        {
            wVISATxt.color = Color.green;
            dVISATxt.color = Color.green;
            fVISATxt.color = Color.green;
            tVISATxt.color = Color.green;
        }
        if (ACCT1 < 0)
        {
            wACC1Txt.color = Color.red;
            dACC1Txt.color = Color.red;
            fACC1Txt.color = Color.red;
            tACC1Txt.color = Color.red;
        }
        else
        {
            wACC1Txt.color = Color.green;
            dACC1Txt.color = Color.green;
            fACC1Txt.color = Color.green;
            tACC1Txt.color = Color.green;
        }
        if (ACCT2 < 0)
        {
            wACC2Txt.color = Color.red;
            dACC2Txt.color = Color.red;
            fACC2Txt.color = Color.red;
            tACC2Txt.color = Color.red;
        }
        else
        {
            wACC2Txt.color = Color.green;
            dACC2Txt.color = Color.green;
            fACC2Txt.color = Color.green;
            tACC2Txt.color = Color.green;
        }
        
        dispWithdraw();
        dispDeposit();
        transFromDisp();
        transToDisp();
	}

	//Button input handlers - when a certain button is clicked, either activate a new screen, or update a numerical value
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log(this.gameObject.name + " Was Clicked.");

			//Return to home screen
			if (this.gameObject.name == "Home") {
                logout();
				selectActionCanvas.SetActive (true);

			} 

			if (this.gameObject.name == "Logout") {
                logout();
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
