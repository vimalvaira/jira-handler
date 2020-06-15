var jh = jh || {};
(function () {

    var _createFormTemplate = null;

    this.getOperations = function (projectKey) {

        _createFormTemplate = null;

        $.ajax({
            url: "http://10.10.30.190:81/Directory/SetDefaultKey",
            method: "post",
            data: { "Key": projectKey },
            cache: false
        }).done(function (data) {
            if (data === "True") {
                $.ajax({
                    url: "http://10.10.30.190:81/Directory/Operations",
                    Get: "Get",
                    Cache: false
                }).done(function (data) {
                    $(".body-content").html(data);


                });
            } else {
                jh.dialog("Opps", "Something went worng!");
            }
        }).fail(function (ex) {
            alert("Error: " + ex);
            console.log(ex);
        });
    };

    this.getCreateIssue = function () {
        $.ajax({
            url: "http://10.10.30.190:81/Directory/CreateSubTaskPage",
            method: "Get",
            cache: false
        }).done(function (data) {
            $(".body-content").html(data);

        }).fail(function (ex) {
            alert("Error: " + ex);
            console.log(ex);
        });
    };

    this.insertCreateSubtaskTemplate = function () {
        console.log(_createFormTemplate);
        if (_createFormTemplate) {
            $(".create-subtask-container").append(_createFormTemplate);
            return;
        }

        $.ajax({
            url: "http://10.10.30.190:81/Directory/GetCreateFormTemplate",
            method: "Get",
            cache: false
        }).done(function (data) {
            $(".create-subtask-container").append(data);
            _createFormTemplate = data;
            // $(".date-input").datepicker();
        }).fail(function (ex) {
            alert("Error: " + ex);
            console.log(ex);
        });

        // $('.create-subtask-container').append($('.create-subtask-template').html());
    };

    this.getInfo = function () {
        let val = $('#parent-task').val();

        if (val) {
            $.ajax({
                url: "http://10.10.30.190:81/Directory/GetIssue?issueNumber=" + val,
                method: "Get",
                cache: false,
                dataType: 'json',
            }).done(function (data) {
                if (data) {
                    $("#parent-summary").text(data.fields.summary);


                } else {
                    alert("counld not find it");
                }
            }).fail(function (ex) {
                alert("Error: " + ex);
                console.log(ex);
            });
        }
    }

    this.submitForm = function () {
        let formCount = $('form').length;

        if (!$('#parent-task').val()) {
            alert('Need a parent task Id')
            return;
        };
        if (!formCount) {
            alert('There is nothing to submit');
            return;
        };


        let formArray = [];
        let formData = {};

        for (let i = 0 ; i < formCount ; i++) {
            formData = {};
            $("form:eq(" + i + ")").serializeArray().map(function (x) { formData[x.name] = x.value; });
            formData.ParentId = $('#parent-task').val();
            formArray.push(formData);
        }
        console.log(formArray);

        $.ajax({
            url: "http://10.10.30.190:81/Directory/CreateSubtast",
            method: "Post",
            contentType: "application/json;charset=utf-8",
            data:    JSON.stringify({'subtasks': formArray}),
            cache: false,


        }).done(function (data) {
            if (data.errors.length) {
                alert("something went wrong!!!");
            } else {
                let string = ""
                $.each(data.issues, function (index, obj) { string = obj.key + " " })
                alert(string);
            }
        }).fail(function (ex) {
            alert("Error: " + ex);
            console.log(ex);
        });

        console.log(JSON.stringify({ 'subtasks': formArray }));

    };


    this.removeFormSection = function (elementObj) {
        elementObj.closest('form').remove();
    };

    this.calulateTime = function (elementObj) {
        let timeInSecond = 0;
        if ($('#day').val()) {
            timeInSecond += $('#day').val() * 7 * 60;
        }
        if ($('#hour').val()) {
            timeInSecond += $('#hour').val() * 60;
        }
        if ($('#minute').val()) {
            timeInSecond += $('#minute').val();
        }
        console.log(timeInSecond);
        elementObj.siblings('.orignal-estimate').val(timeInSecond);
        // elementObj.closest('.row').find('#orignal-estimate').val(timeInSecond);
    };

}).apply(jh.directory = jh.directory || {});

