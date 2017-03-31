#!/usr/bin/env python

import requests


def getStockQuote(symbol):
    quoteUrl = "http://finance.yahoo.com/d/quotes.csv?f=l1&s=" + symbol
    r = requests.get(quoteUrl)

    # Let's give up if something went wrong 
    if r.status_code != 200:
        return None

    currentPrice = float(r.content)
    return currentPrice


def sendTelegramMessage(targetNumber, message):
    # TODO: When you have your own Client ID and secret, put down their values here:
    clientId = "YOUR_CLIENT_ID_HERE"
    clientSecret = "YOUR_CLIENT_SECRET_HERE"

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
    # Get the stock quote of Apple
    symbol = "AAPL"
    price = getStockQuote(symbol)
    if price < 120:
        print("My stock is still below my target price. Exiting now...")
        quit()
    else:
        print("My stock has hit my target price!. Will try to send a Telegram message.")

    # Try to send a Telegram message
    number =  '12025550108'  # FIXME: Put down your registered number here
    message = "My beloved stock " + symbol + " is selling at $" + str(price) + " now. Take the profits and get rich!"
    boolMessageSent = sendTelegramMessage(number, message)

    if boolMessageSent == True:
        print("The price alert was sent successfully.")
    else:
        print("The price alert was NOT sent successfully.")


#####################
# Main Logic
#####################
if __name__ == "__main__":
    # execute only if run as a script
    main()
