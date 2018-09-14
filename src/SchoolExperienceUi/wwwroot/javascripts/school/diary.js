const freeBackgroundColour = 'lightgreen';
const freeTextColour = 'black';
const freeText = 'Available';
const busyBackgroundColour = 'lightpurple';
const busyTextColour = 'white';
const DeleteDiaryEntryResult = {
    None: 0,
    Deleted: 1,
    NotFound: 2,
    NotAllowed: 3
};

const schoolId = '22222222-2222-2222-2222-222222222222';

$(function () {

    // page is now ready, initialize the calendar...

    var calendar = $("#calendar").fullCalendar({
        weekends: false, // will hide Saturdays and Sundays
        defaultView: 'agendaWeek',
        events: {
            url: '/school/events',
            type: 'GET',
            data: { 'schoolId': schoolId },
            success: function (result) {
                $(result.events).each(function (index, item) {
                    if (item.isFree) {
                        item.title = freeText;
                        item.color = freeBackgroundColour;
                        item.textColor = freeTextColour;
                    }
                    if (item.isBusy) {
                        item.color = busyBackgroundColour;
                        item.textColor = busyTextColour;
                    }
                });
                return result.events;
            },
            color: 'blue',
            textColor: 'white'
        },
        eventClick: function (calEvent, jsEvent, view) {
            eventClick(calEvent);
        },
        error: function () {
            alert('there was an error while fetching events!');
        }
    });

    function eventClick(event) {
        if (event.isFree) {
            bookCandidate(event.candidateId, event.date);
        }
        if (event.isBusy) {
            unbookEvent('a');
        }
    }

    function removeEvent(event) {
        $.ajax({
            url: '/school/unbookDiaryEntry',
            method: 'DELETE',
            data: { 'id': event.id }
        })
            .done(function (response) {
                if (response === DeleteDiaryEntryResult.Deleted) {
                    calendar.fullCalendar('removeEvents', function (item) {
                        return item.id === event.id;
                    });
                }
            });
    }

    function bookCandidate(candidateId, date) {
        $.ajax({
            url: '/school/bookCandidate',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ 'schoolId': schoolId, 'candidateId': candidateId, 'date': date.format() })
        })
            .done(function (response) {
                var events = [];

                $(response.events).each(function (index, item) {
                    switch (item.result) {
                        case 1: // 'Success':
                            events.push(
                                {
                                    id: item.id,
                                    isFree: true,
                                    title: freeText,
                                    start: item.when,
                                    allDay: true,
                                    color: freeBackgroundColour,
                                    textColor: freeTextColour
                                });
                            break;

                        case 2: //'Conflict':
                            break;
                    }
                });

                if (events.length > 0) {
                    calendar.fullCalendar('addEventSource', events);
                }
            })
            .fail(function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            });
    }
});
