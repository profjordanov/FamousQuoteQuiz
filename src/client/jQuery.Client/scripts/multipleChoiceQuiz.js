(function () {
  $(document).ready(function() {
    const querydata = {
      initialId: 0
    };
    const url = baseSurviceQuizUrl + "multiple-choice-question" + encodeQueryData(querydata);
    $.ajax({
      url: url,
      type: 'GET',
      responseType: 'application/json',
      success: function(data) {
        applyMultipleChoiceQuestion(data);
      },
      error: function() {
        $.fancybox("#error");
      }
    });

  });
})(baseSurviceQuizUrl);

(function () {
  $(document).ready(function() {
    $("#authorX").click(function () {
      const authorXid = $("#authorX").attr('authorId');
      checkCorrectAuthorById(authorXid);
    });
  });
})();

(function () {
  $(document).ready(function() {
    $("#authorY").click(function () {
      const authorYid = $("#authorY").attr('authorId');
      checkCorrectAuthorById(authorYid);
    });
  });
})();

(function () {
  $(document).ready(function() {
    $("#authorZ").click(function () {
      const authorZid = $("#authorZ").attr('authorId');
      checkCorrectAuthorById(authorZid);
    });
  });
})();

function checkCorrectAuthorById (authorNid) {
  const questId = $("#mult-quest-id").text();
  const correctAuthorId = $("#correct-author-id").text();
  const correctAuthorName = $("#correct-author-name").text();
  const lastMultipleChoiceQuestionId = localStorage.lastMultipleChoiceQuestionId;

  if(correctAuthorId == authorNid){
    if(questId == lastMultipleChoiceQuestionId){
      showFinalCorrectUserAnswer(correctAuthorName);
    }else{
      showCorrectUserAnswer(correctAuthorName);
      showNextMultQuestBtn();
    }
  }else{
    if(questId == lastMultipleChoiceQuestionId){
      showFinalIncorrectUserAnswer(correctAuthorName);
    }else{
      showIncorrectUserAnswer(correctAuthorName);
      showNextMultQuestBtn();
    }
  }
}

(function() {
  $(document).ready(function() {

    $("#next-mult-quest-btn").click(function() {
      const currentQuoteId = $("#mult-quest-id").text();

      const querydata = {
        initialId: currentQuoteId
      };

      const url = baseSurviceQuizUrl + "multiple-choice-question" + encodeQueryData(querydata);

      $.ajax({
        url: url,
        type: 'GET',
        responseType: 'application/json',
        success: function(data) {
          applyMultipleChoiceQuestion(data);
          showAnswersBtns();
        },
        error: function() {
          $.fancybox("#error");
        }
      });
    });
  });
})();

function applyMultipleChoiceQuestion(data) {
  $("#mult-quest-id").text(data.id);
  $("#correct-author-id").text(data.correctAuthorId);
  $("#mult-quote-content").text(data.quote);
  let authors = data.authors;

  var correctAuthor = authors.find(function(element) {
    return element.id == data.correctAuthorId;
  });

  $("#correct-author-name").text(correctAuthor.name);

  $("#authorX").text(authors[0].name);
  $("#authorX").attr('authorId', authors[0].id);

  $("#authorY").text(authors[1].name);
  $("#authorY").attr('authorId', authors[1].id);

  $("#authorZ").text(authors[2].name);
  $("#authorZ").attr('authorId', authors[2].id);
}

function showNextMultQuestBtn() {
  const correctAuthor = $("#correct-author-name").text();
  $("#mult-quote-correct-answer").show();
  $("#mult-quote-correct-answer").text("by " + correctAuthor);

  $("#next-mult-quest-btn").show();
  $("#answers-buttons").hide();
}

function showAnswersBtns() {
  $("#mult-quote-correct-answer").hide();

  $("#answers-buttons").show();
  $("#next-mult-quest-btn").hide();
}