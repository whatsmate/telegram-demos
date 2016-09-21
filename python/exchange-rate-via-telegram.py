#!/usr/bin/env python

import requests


def getExchangeRate(baseCurrency, targetCurrency):
    fxUrl = "http://api.fixer.io/latest?base=" + baseCurrency + "&symbols=" + targetCurrency
    fxReq = requests.get(fxUrl)

    # Let's give up if something went wrong 
    if fxReq.status_code != 200:
        return None

    # If we are still here, everything should have gone well
    exchangeRate = fxReq.json().get('rates').get(targetCurrency)
    return exchangeRate


def sendTelegramMessage(targetNumber, message):
    # TODO: When you have your own Client ID and secret, put down their values here:
    clientId = "FREE_TRIAL_ACCOUNT"
    clientSecret = "PUBLIC_SECRET"

    jsonBody = {
        'number': targetNumber,
        'message': message
    }

    headers = {
        'X-WM-CLIENT-ID': clientId, 
        'X-WM-CLIENT-SECRET': clientSecret
    }

    r = requests.post('http://api.whatsmate.net/v1/telegram/single/message/0',
        headers=headers,
        json=jsonBody)

    boolSuccess = False
    if r.status_code == 200:
        boolSuccess = r.json().get('success')

    return boolSuccess


def main():
    # Get the exchange rate of Japanese YEN against USD
    yenExRate = getExchangeRate("USD", "JPY")

    # Try to send a Telegram message
    number =  '12025550108'  # FIXME: Put down your registered number here
    message = 'Exchange rate of YEN for today is ' + str(yenExRate)
    boolMessageSent = sendTelegramMessage(number, message)

    if boolMessageSent == True:
        print("Exchange rate info was sent successfully.")
    else:
        print("Exchange rate info was NOT sent successfully.")


#####################
# Main Logic
#####################
if __name__ == "__main__":
    # execute only if run as a script
    main()
