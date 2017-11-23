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
	public GameObject depositCanvas;
	public GameObject successCanvas;
	public GameObject withdrawConfirmationCanvas;
	public GameObject depositConfirmationCanvas;
	public GameObject depositConfirmationCanvas2;
	public GameObject transferConfirmationCanvas;
	public GameObject introCanvas;
	public GameObject loginCanvas;

	//On screen text, which updates as users click buttons
	public Text withdrawAmntTxt, fromAcctTxt, toAcctTxt, depositAcctTxt, fromCHQTxt, 
	fromSAVTxt, fromVISATxt, fromACC1Txt, fromACC2Txt,
		toCHQTxt, toSAVTxt, toVISATxt, toACC1Txt, toACC2Txt;

	//Numerical monetary values
	public static double CHQ, SAV, VISA, ACCT1, ACCT2 = 1000.00;
	public static double withdrawAmnt, depositAmnt, transferAmnt = 0.00;
	public static string toAcct, fromAcct, withdrawAcct, depositAcct;

	void Start(){
		CHQ = 1000.00;
		SAV = 400.00;
		VISA = -100.00;
		ACCT1 = 10.00;
		ACCT2 = 500.00;
	}
	// Use this for initialization
	void OnEnable () {

		//Only one screen can stay active at a time
		if (this.gameObject.name == "SelectActionCanvas") {
			introCanvas.SetActive (false);
			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			loginCanvas.SetActive (false);

		} else if (this.gameObject.name == "TransferCanvas") {

			selectActionCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);
		} else if (this.gameObject.name == "WithdrawCanvas") {

			transferCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);
		} else if (this.gameObject.name == "DepositCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);
		} else if (this.gameObject.name == "SuccessCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);

		} else if (this.gameObject.name == "WithdrawConfirmationCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);

		} else if (this.gameObject.name == "DepositConfirmationCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			introCanvas.SetActive (false);
			loginCanvas.SetActive (false);
		} 
		else if (this.gameObject.name == "IntroCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			loginCanvas.SetActive (false);
		} 
		else if (this.gameObject.name == "LoginCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			introCanvas.SetActive (false);
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
	

//		CHQTxt.text = CHQ.ToString ();
//		if (this.gameObject.name == "chqtxt") {
//			CHQTxt.text = CHQ.ToString();
//		}
//
//		if (this.gameObject.name == "SAVTxt") {
//			SAVTxt.text = SAV.ToString();
//		}
//
//		if (this.gameObject.name == "VISATxt") {
//			VISATxt.text = VISA.ToString();
//		}
//
//		if (this.gameObject.name == "Acct1Txt") {
//			Acct1Txt.text = ACCT1.ToString();
//		}
//
//		if (this.gameObject.name == "Acct2Txt") {
//			Acct2Txt.text = ACCT2.ToString();
//		}
//
		fromAcct = fromAcctTxt.text;
		toAcct = toAcctTxt.text;
		depositAcct = depositAcctTxt.text;
		withdrawAcct = withdrawAmntTxt.text;

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

					withdrawConfirmationCanvas.gameObject.SetActive(true);
				}

			}

			else if (depositCanvas.activeInHierarchy == true) {

				if (this.gameObject.name == "CHQ") {

					depositAcctTxt.text = "CHQ";
				}

				if (this.gameObject.name == "SAV") {

					depositAcctTxt.text = "SAV";
				}

				if (this.gameObject.name == "VISA") {

					depositAcctTxt.text = "VISA";
				}

				if (this.gameObject.name == "ACCT1") {

					depositAcctTxt.text = "ACCT1";
				}

				if (this.gameObject.name == "ACCT2") {

					depositAcctTxt.text = "ACCT2";
				}

				if (this.gameObject.name == "Confirm") {

					depositConfirmationCanvas.gameObject.SetActive(true);
				}

			}
//
//			else if (successCanvas.activeInHierarchy == true) {
//			
//				if (this.gameObject.name == "Return") {
//
//					selectActionCanvas.gameObject.SetActive (true);
//					successCanvas.gameObject.SetActive(false);
//				}
//			}
//
//			else if (withdrawConfirmationCanvas.activeInHierarchy == true) {
//
//				if (this.gameObject.name == "Confirm") {
//
//					if (withdrawAcct == "CHQ") {
//						CHQ -= withdrawAmnt;
//					}
//
//					//TODO Repeat for other accounts and display the amount
//				}
//				
//			}
//
//			else if (depositConfirmationCanvas.activeInHierarchy == true) {
//
//				if (this.gameObject.name == "Confirm") {
//
//					depositConfirmationCanvas2.gameObject.SetActive(true);
//					depositConfirmationCanvas.gameObject.SetActive(true);
//				}
//
//				if (this.gameObject.name == "Cancel") {
//
//					depositCanvas.gameObject.SetActive (true);
//					depositConfirmationCanvas.gameObject.SetActive(false);
//				}
//			}
//
//			else if (depositConfirmationCanvas2.activeInHierarchy == true) {
//
//				if (this.gameObject.name == "Confirm") {
//
//					if (depositAcct == "CHQ") {
//						CHQ += Random.Range (20, 2000);
//					}
//
//				//TODO Repeat for other accounts and display the amount
//				}
//
//				if (this.gameObject.name == "Cancel") {
//
//					depositCanvas.gameObject.SetActive (true);
//					depositConfirmationCanvas.gameObject.SetActive(false);
//				}
//			}
//
//			else if (transferCanvas.activeInHierarchy == true) {
//
//				if (this.gameObject.name == "FROMCHQ") {
//
//					fromAcctTxt.text = "CHQ";
//				}
//
//				if (this.gameObject.name == "FROMSAV") {
//
//					fromAcctTxt.text = "SAV";
//				}
//
//				if (this.gameObject.name == "FROMVISA") {
//					
//					fromAcctTxt.text = fromAcct = "VISA";
//				}
//
//				if (this.gameObject.name == "FROMACCT1") {
//					
//					fromAcctTxt.text = "ACCT1";
//				}
//
//				if (this.gameObject.name == "FROMACCT2") {
//
//					fromAcctTxt.text = "ACCT2";
//				}
//
//
//				if (this.gameObject.name == "TOCHQ") {
//
//					toAcctTxt.text = "CHQ";
//				}
//
//				if (this.gameObject.name == "TOSAV") {
//
//					toAcctTxt.text = "SAV";
//				}
//
//				if (this.gameObject.name == "TOVISA") {
//
//					toAcctTxt.text = "VISA";
//				}
//
//				if (this.gameObject.name == "TOACCT1") {
//
//					toAcctTxt.text = "ACCT1";
//				}
//
//				if (this.gameObject.name == "TOACCT2") {
//
//					toAcctTxt.text = "ACCT2";
//				}
//
//				if (this.gameObject.name == "Confirm") {
//
//					transferConfirmationCanvas.gameObject.SetActive(true);
//				}
//
//			}
//
//			else if (transferConfirmationCanvas.activeInHierarchy == true) {
//
//				if (this.gameObject.name == "Confirm") {
//
//					if (fromAcct == "CHQ") {
//					
//						if (toAcct == "SAV") {
//							SAV += transferAmnt;
//							CHQ -= transferAmnt;
//						}
//					
//					//TODO Repeat for other accounts and display the amount
//				}
//			}
//
//				if (this.gameObject.name == "Cancel") {
//
//					depositCanvas.gameObject.SetActive (true);
//					depositConfirmationCanvas.gameObject.SetActive(false);
//				}
//		}
	}
}
