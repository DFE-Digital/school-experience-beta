import * as React from 'react';
import { Link } from 'react-router-dom';

export class Dashboard extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            advertised: 150,
            booked: 99,
            available: 51,
            subjects: [
                { "Id": 1, "Name": "English", "Count": 26 },
                { "Id": 2, "Name": "Maths", "Count": 24 }
            ],
            schools: [
                { "Id": 1, "Name": "Eaton", "Count": 10 },
                { "Id": 99, "Name": "Hogwarts", "Count": 40 }
            ]
        };
    }

    getSubjectRows(subjects) {
        const rows = subjects.map(row =>
            (
                <tr className="govuk-table__row" key={row.Id}>
                    <th className="govuk-table__header" scope="row">{row.Name}</th>
                    <td className="govuk-table__cell govuk-table__cell--numeric">{row.Count}</td>
                </tr>
            ));

        return rows;
    }

    getSubjectTable(subjects) {
        const table = (
            <table className="govuk-table">
                <caption className="govuk-table__caption">Available placements</caption>
                <thead className="govuk-table__head">
                    <tr className="govuk-table__row">
                        <th className="govuk-table__header" scope="col">Subject</th>
                        <th className="govuk-table__header govuk-table__header--numeric" scope="col">Amount</th>
                    </tr>
                </thead>
                <tbody className="govuk-table__body">
                    {this.getSubjectRows(this.state.subjects)}
                </tbody>
            </table>
        );

        if (subjects.length > 0) {
            return table;
        }

        return (
            <div>
                <hr />
                <p className="">No placements available</p>

                <hr />
            </div>
        );
    }

    getSchoolRows(schools) {
        const rows = schools.map(school =>
            (
                <tr className="govuk-table__row" key={school.Id}>
                    <th className="govuk-table__header" scope="row">{school.Name}</th>
                    <td className="govuk-table__cell govuk-table__cell--numeric">{school.Count}</td>
                </tr>
            ));

        return rows;
    }

    getSchoolTable(schools) {
        const table = (
            <table className="govuk-table">
                <caption className="govuk-table__caption">Available placements by school</caption>
                <thead className="govuk-table__head">
                    <tr className="govuk-table__row">
                        <th className="govuk-table__header" scope="col">Name</th>
                        <th className="govuk-table__header govuk-table__header--numeric" scope="col">Amount</th>
                    </tr>
                </thead>
                <tbody className="govuk-table__body">
                    {this.getSchoolRows(schools)}
                </tbody>
            </table>


        )

        if (schools.length > 0) {
            return table;
        }

        return;
    }
    render() {
        return (
            <main className="govuk-main-wrapper " id="main-content" role="main">



                <div className="govuk-grid-row dashboard-figures">


                    <div className="govuk-grid-column-one-half">


                        <h1 className="govuk-heading-m">Your placements</h1>
                        <div className="top-data-elements">
                            <p className="govuk-!-font-size-24 key-measure">
                                <em className="govuk-!-font-size-48 keyfigure">{this.state.advertised}</em><br />
                                Advertised</p>
                        </div>
                        <div className="top-data-elements">
                            <p className="govuk-!-font-size-24 key-measure">
                                <em className="govuk-!-font-size-48 keyfigure">{this.state.booked}</em><br />
                                Booked</p>
                        </div>
                        <div className="top-data-elements">

                            <p className="govuk-!-font-size-24 key-measure">
                                <em className="govuk-!-font-size-48 keyfigure">{this.state.available}</em><br />
                                Available</p>
                        </div>


                        {this.getSubjectTable(this.state.subjects)}

                        {this.getSchoolTable(this.state.schools)}

                    </div>



                    <div className="govuk-grid-column-one-half">

                        <div className="dashboard-top-divider">
                            &nbsp;
            </div>

                        <div className="greyblock1">

                            <h2 className="govuk-heading-m">Your associations</h2>
                            <ul className="govuk-list">
                                <li><a className="govuk-link" href="/booking/associate?id=2">View associations</a></li>
                                <li><a className="govuk-link" href="/booking/add-subsidiary?id=2">Add subsidiary schools</a></li>
                                <li><a className="govuk-link" href="/booking/remove-subsidiary?id=2">Remove subsidiary schools</a></li>
                            </ul>

                        </div>
                        <div className="greyblock2">

                            <h2 className="govuk-heading-m">Your placements</h2>
                            <ul className="govuk-list">
                                <li><a className="govuk-link" href="/booking/view-calender?id=4">View calendar</a></li>
                                <li><a className="govuk-link" href="/booking/non-placements?id=4">Add non-placement days</a></li>


                                <li><a className="govuk-link" href="/booking/current-placments?id=4">View live placements</a></li>
                                <li><a className="govuk-link" href="/booking/manage-placements?id=4">Manage placements</a></li>

                            </ul>

                        </div>


                        <div className="greyblock1">

                            <h2 className="govuk-heading-m">Your profiles</h2>
                            <ul className="govuk-list">
                                <li><a className="govuk-link" href="/booking/profile?id=3">View profiles</a></li>
                                <li><a className="govuk-link" href="/booking/image-management?id=3">Image management</a></li>
                                <li><a className="govuk-link" href="/booking/add-content?id=3">Add content</a></li>
                                <li><a className="govuk-link" href="/booking/remove-content?id=3">Remove content</a></li>
                            </ul>

                        </div>

                        <div className="greyblock2">

                            <h2 className="govuk-heading-m">Your bookings</h2>
                            <ul className="govuk-list">
                                <li><a className="govuk-link" href="/booking/view-bookings?id=5">View bookings</a></li>
                                <li><a className="govuk-link" href="/booking/view-messages?id=5">View messages</a></li>
                                <li><a className="govuk-link" href="/booking/notification?id=5">Notifications</a></li>
                                <li><a className="govuk-link" href="/booking/attenedance?id=5">Candidate attendance</a></li>
                            </ul>

                        </div>



                    </div>

                </div>

            </main>
        )
    }

}