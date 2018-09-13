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

$(function () {

    // page is now ready, initialize the calendar...

    var calendar = $("#calendar").fullCalendar({
        weekends: false, // will hide Saturdays and Sundays
        defaultView: "month",
        allDayDefault: true,
        selectable: true,
        events: {
            url: '/candidate/events',
            type: 'GET',
            data: {},
            success: function (result) {
                $(result.events).each(function (index, item) {
                    item.allDay = true;
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
        select: function (startDate, endDate) {
            selectDays(startDate, endDate);
        },
        error: function () {
            alert('there was an error while fetching events!');
        }
    });

    function eventClick(event) {
        if (event.isFree) {
            removeEvent(event);
        }
        if (event.isBusy) {
            alert('a');
        }
    }

    function removeEvent(event) {
        $.ajax({
            url: '/candidate/removeDiaryEntry',
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

    function selectDays(startDate, endDate) {
        $.ajax({
            url: '/candidate/creatediaryentry',
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ 'start': startDate.format(), 'end': endDate.format(), 'fred':1 })
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
