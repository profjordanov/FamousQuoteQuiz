const baseSurviceQuizUrl = "http://localhost:5000/api/quiz/";

document.addEventListener('deviceready', onDeviceReady, false);

function onDeviceReady() {

}

(function () {
    $.ajax({
        url: baseSurviceQuizUrl + "last-binary-choice-question",
        type: 'GET',
        responseType: 'application/json',
        success: function (data) {
            sessionStorage.setItem("last-question-id", data.id);
        },
        error: function () {
            alert("error");
        }
    });
})(baseSurviceQuizUrl);

(function () {
    $.ajax({
        url: baseSurviceQuizUrl + "binary-choice-question?initialId=0",
        type: 'GET',
        responseType: 'application/json',
        success: function (data) {
            $('#bin-quote-id').text(data.id);
            $('#bin-quote-isTrue').text(data.isTrue);
            $('#bin-quote-correct-author').text(data.correctAuthor);
            $("#bin-quote-content").text(data.quote);
            $("#bin-quote-author").text(data.questionableAuthor);
            $("#bin-quote-correct-author").text(data.correctAuthor);
        },
        error: function () {
            alert("error");
        }
    });
})(baseSurviceQuizUrl);

$("#true-bin-answe-btn").click(function () {
    const currentQuestionId = $("#bin-quote-id").text();
    const questIsTrue = $("#bin-quote-isTrue").text();
    const correctAuthor = $("#bin-quote-correct-author").text();
    if (questIsTrue == "true") {
        showBcqCorrectUserAnswer(correctAuthor);
        showNextBcqBtn();
    } else {
        showBcqIncorrectUserAnswer(correctAuthor);
        showNextBcqBtn();
    }
});

$("#false-bin-answe-btn").click(function () {
    const currentQuestionId = $("#bin-quote-id").text();
    const questIsTrue = $("#bin-quote-isTrue").text();
    const correctAuthor = $("#bin-quote-correct-author").text();
    if (questIsTrue == "true") {
        showBcqIncorrectUserAnswer(correctAuthor);
        showNextBcqBtn();
    } else {
        showBcqCorrectUserAnswer(correctAuthor);
        showNextBcqBtn();
    }
});

$("#next-bin-quest-btn").click(function () {
    const currentQuoteId = $("#bin-quote-id").text();
    $.ajax({
        url: baseSurviceQuizUrl + "binary-choice-question?initialId=" + currentQuoteId,
        type: 'GET',
        responseType: 'application/json',
        success: function (data) {
            $('#bin-quote-id').text(data.id);
            $('#bin-quote-isTrue').text(data.isTrue);
            $('#bin-quote-correct-author').text(data.correctAuthor);
            $("#bin-quote-content").text(data.quote);
            $("#bin-quote-author").text(data.questionableAuthor);
            $("#bin-quote-correct-author").text(data.correctAuthor);
            showYesNoBtns();
        },
        error: function () {
            alert("error");
        }
    });
});

function showBcqCorrectUserAnswer(correctAuthor) {
    $("#bcq-correct-author-answer").text(correctAuthor);
    $("#bcq-correct-user-answer-popup").popup("open");
}

function showBcqIncorrectUserAnswer(correctAuthor) {
    $("#bcq-incorrect-author-answer").text(correctAuthor);
    $("#bcq-incorrect-user-answer-popup").popup("open");
}

function showYesNoBtns() {
    $("#bin-quote-correct-answer").hide();
    $("#who-is").show();

    $("#yes-no-buttons").show();
    $("#next-bin-quest-btn").hide();
}

function showNextBcqBtn() {
    $("#who-is").hide();
    const correctAuthor = $("#bin-quote-correct-author").text();
    $("#bin-quote-correct-answer").show();
    $("#bin-quote-correct-answer").text("by " + correctAuthor);

    $("#next-bin-quest-btn").show();
    $("#yes-no-buttons").hide();
}