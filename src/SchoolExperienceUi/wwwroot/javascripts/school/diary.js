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
const candidateId = '11111111-1111-1111-1111-111111111111';

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
                    if (item.isBusy) {
                        item.color = busyBackgroundColour;
                        item.textColor = busyTextColour;
                        }
                    else {
                        item.color = freeBackgroundColour;
                        item.textColor = freeTextColour;
                    }
                });
                return result.events;
            },
            color: 'blue',
            textColor: 'white'
        },

        selectable: true,
        selectHelper: true,
        select: function (start, end) {
            bookCandidate(candidateId, start);
            $('#calendar').fullCalendar('unselect');
        },
        eventClick: function (calEvent, jsEvent, view) {
            eventClick(calEvent);
        },
        error: function () {
            alert('there was an error while fetching events!');
        }
    });

    function eventClick(event) {
        //if (!event.isFree) {
        //}
        if (event.isBusy) {
            removeEvent(event);
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
            data: JSON.stringify({ 'schoolId': schoolId, 'candidateId': candidateId, 'when': date.format() })
        })
            .done(function (response) {
                var event = {
                    id: response.id,
                    isFree: true,
                    title: response.text,
                    start: date,
                    allDay: false,
                    color: freeBackgroundColour,
                    textColor: freeTextColour
                };

                calendar.fullCalendar('renderEvent', event, true); // stick? = true
            })
            .fail(function (jqXHR, textStatus) {
                alert("Request failed: " + textStatus);
            });
    }
});
