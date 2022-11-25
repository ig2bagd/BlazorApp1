class SurveyPrompt {
    displayAlert(message) {
        alert(message);
    }
    displayPrompt(message, defaultValue) {
        return prompt(message, defaultValue);
    }
    log(message) {
        console.log(message);
    }
}
export function GetInstance() {
    return new SurveyPrompt();
}
//# sourceMappingURL=SurveyPrompt.razor.js.map