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

	//On screen text, which updates as users click buttons
	public Text withdrawAmntTxt, fromAcctTxt, toAcctTxt, depositAcctTxt, CHQTxt, SAVTxt, VISATxt, Acct1Txt, Acct2Txt;

	//Numerical monetary values
	public static double CHQ, SAV, VISA, ACCT1, ACCT2, withdrawAmnt, depositAmnt = 0.00;

	// Use this for initialization
	void OnEnable () {

		//Only one screen can stay active at a time
		if (this.gameObject.name == "SelectActionCanvas") {
			
			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);

		} else if (this.gameObject.name == "TransferCanvas") {

			selectActionCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);

		} else if (this.gameObject.name == "WithdrawCanvas") {

			transferCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);

		} else if (this.gameObject.name == "DepositCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);

		} else if (this.gameObject.name == "SuccessCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);

		} else if (this.gameObject.name == "WithdrawConfirmationCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
			depositConfirmationCanvas.SetActive (false);

		} else if (this.gameObject.name == "DepositConfirmationCanvas") {

			transferCanvas.SetActive (false);
			withdrawCanvas.SetActive (false);
			depositCanvas.SetActive (false);
			successCanvas.SetActive (false);
			withdrawConfirmationCanvas.SetActive (false);
			selectActionCanvas.SetActive (false);
		} 
	}
	
	// Update is called once per frame
	void Update () {

		//Constantly update text displays
		if (this.gameObject.name == "WithdrawAmntTxt") {
			withdrawAmntTxt.text = withdrawAmnt.ToString();
		}

		/*if (this.gameObject.name == "CHQTxt") {
			CHQTxt.text = withdrawAmnt.ToString();
		}

		if (this.gameObject.name == "SAVTxt") {
			SAVTxt.text = withdrawAmnt.ToString();
		}

		if (this.gameObject.name == "VISATxt") {
			VISATxt.text = withdrawAmnt.ToString();
		}

		if (this.gameObject.name == "Acct1Txt") {
			Acct1Txt.text = withdrawAmnt.ToString();
		}

		if (this.gameObject.name == "Acct2Txt") {
			Acct2Txt.text = withdrawAmnt.ToString();
		}*/

	}

	//Button input handlers - when a certain button is clicked, either activate a new screen, or update a numerical value
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log(this.gameObject.name + " Was Clicked.");


			//TODO Back and Logout functionality
			if (this.gameObject.name == "Back") {

			} 

			//Return to home screen
			if (this.gameObject.name == "Home") {
				selectActionCanvas.SetActive (true);

			} 

			if (this.gameObject.name == "Logout") {

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

			}

			else if (successCanvas.activeInHierarchy == true) {

			}

			else if (withdrawConfirmationCanvas.activeInHierarchy == true) {

				

			}

			else if (depositConfirmationCanvas.activeInHierarchy == true) {

			}

			else if (transferCanvas.activeInHierarchy == true) {

				if (this.gameObject.name == "FROMCHQ") {

					fromAcctTxt.text = "CHQ";
				}

				if (this.gameObject.name == "FROMSAV") {

					fromAcctTxt.text = "SAV";
				}

				if (this.gameObject.name == "FROMVISA") {
					
					fromAcctTxt.text = "VISA";
				}

				if (this.gameObject.name == "FROMACCT1") {
					
					fromAcctTxt.text = "ACCT1";
				}

				if (this.gameObject.name == "FROMACCT2") {

					fromAcctTxt.text = "ACCT2";
				}


				if (this.gameObject.name == "TOCHQ") {

					toAcctTxt.text = "CHQ";
				}

				if (this.gameObject.name == "TOSAV") {

					toAcctTxt.text = "SAV";
				}

				if (this.gameObject.name == "TOVISA") {

					toAcctTxt.text = "VISA";
				}

				if (this.gameObject.name == "TOACCT1") {

					toAcctTxt.text = "ACCT1";
				}

				if (this.gameObject.name == "TOACCT2") {

					toAcctTxt.text = "ACCT2";
				}


			}

		}
			
}
