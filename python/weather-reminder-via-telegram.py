#!/usr/bin/env python

import requests


def getWeatherCondition(city):
    # Code adopted from https://developer.yahoo.com/weather/
    import urllib2, urllib, json
    baseurl = "https://query.yahooapis.com/v1/public/yql?"
    yql_query = 'select item.condition.text from weather.forecast where woeid in (select woeid from geo.places(1) where text="' + city + '")'
    yql_url = baseurl + urllib.urlencode({'q':yql_query}) + "&format=json"
    result = urllib2.urlopen(yql_url).read()
    data = json.loads(result)
    weatherJson = data['query']['results']
    # Example JSON: {u'channel': {u'item': {u'condition': {u'text': u'Mostly Clear'}}}}
    weatherConditionText = weatherJson.get('channel').get('item').get('condition').get('text')
    return weatherConditionText


def sendTelegramMessage(targetNumber, message):
    # TODO: When you have your own Client ID and secret, put down their values here:
    instanceId = "YOUR_GATEWAY_INSTANCE_ID_HERE"
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

    r = requests.post("http://api.whatsmate.net/v1/telegram/single/message/%s" % instanceId,
        headers=headers,
        json=jsonBody)

    boolSuccess = False
    if r.status_code == 200:
        boolSuccess = r.json().get('success')

    return boolSuccess


def main():
    # Get the weather info for Mountain View, California
    city = "mountain view, ca"
    weatherConditionText = getWeatherCondition(city)

    # Try to send a Telegram message
    number =  '12025550108'  # FIXME: Put down your registered number here
    message = "Weather in Mountain View: " + weatherConditionText
    boolMessageSent = sendTelegramMessage(number, message)

    if boolMessageSent == True:
        print("Weather info was sent successfully.")
    else:
        print("Weather info was NOT sent successfully.")


#####################
# Main Logic
#####################
if __name__ == "__main__":
    # execute only if run as a script
    main()
