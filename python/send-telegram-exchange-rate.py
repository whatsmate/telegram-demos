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


#############
# Main logic
#############

# TODO: When you have your own Client ID and secret, put down their values here:
clientId = "FREE_TRIAL_ACCOUNT"
clientSecret = "PUBLIC_SECRET"

# Get the exchange rate of Japanese YEN against USD
yenExRate = getExchangeRate("USD", "JPY")

jsonBody = {
    'number': '12025550108',  # FIXME: Put down your registered number here
    'message': 'Exchange rate of YEN for today is ' + str(yenExRate)
}

headers = {
    'X-WM-CLIENT-ID': clientId, 
    'X-WM-CLIENT-SECRET': clientSecret
}

r = requests.post('http://api.whatsmate.net/v1/telegram/single/message/0',
    headers=headers,
    json=jsonBody)

print("Status code: " + str(r.status_code))
print("Response: " + str(r.content))

