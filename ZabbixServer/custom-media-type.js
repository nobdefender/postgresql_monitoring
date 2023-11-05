function sendMessage(params) {
    // Declaring variables
    var response,
        request = new HttpRequest();

    // Adding the required headers to the request
    request.addHeader('Content-Type: application/json');
    request.addHeader('Authorization: Bearer ' + params.bot_token);

	var msg = 'test'
	if (params.event_source === '0' && params.event_value === '1') {
        msg = params.alert_subject + '\n\n' + params.alert_message + '\n' + params.trigger_description;
    }
	
    // Forming the request that will send the message
    response = request.post('http://localhost:5298/UserAction/api/UserAction/Push', JSON.stringify({
	  "message": msg,
	  "userActionModels": JSON.parse(params.user_actions)
	}));

    // If the response is different from 200 (OK), return an error with the content of the response
    if (request.getStatus() !== 200) {
        throw "API request failed: " + response;
    }
}

function validateParams(params) {
    // Checking that the bot_token parameter is a string and not empty
    if (typeof params.bot_token !== 'string' || params.bot_token.trim() === '') {
        throw 'Field "bot_token" cannot be empty';
    }

    // Checking that the event_source parameter is only a number from 0-3
    if (parseInt(params.event_source) !== 0) {
        throw 'Incorrect "event_source" parameter given: "' + params.event_source + '".nMust be 0.';
    }

    // Checking that trigger_id is a number and not equal to zero
    if (isNaN(params.trigger_id) && params.event_source === '0') {
        throw 'field "trigger_id" is not a number';
    }
}

try {
    // Declaring the params variable and writing the webhook parameters to it
    var params = JSON.parse(value);

    // Calling the validation function and passing parameters to it for verification
    validateParams(params);

    // Sending a composed message
    sendMessage(params);

    // Returning OK so that the webhook understands that the script has completed with OK status
    return 'OK';
}
catch (err) {
    // Adding a log function so in case of problems you can see the error in the Zabbix server console
    Zabbix.log(4, '[ Custom Webhook ] Line notification failed : ' + err);

    // In case of an error, return it from the webhook
    throw 'Custom notification failed : ' + err;
}