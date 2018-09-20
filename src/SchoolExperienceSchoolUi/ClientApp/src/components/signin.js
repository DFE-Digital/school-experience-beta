import * as React from 'react';
import { Link , Redirect} from 'react-router-dom';
import axios from 'axios';

const baseUrl = 'https://schoolexperiencebetaapi.azurewebsites.net/';

export class SignIn extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            userId: "",
            password: "",
            toDashboard: false,
        };
    }

    validateForm() {
        return this.state.userId.length > 0 && this.state.password.length > 0;
    }

    handleChange = event => {
        this.setState({
            [event.target.id]: event.target.value
        });
    }

    handleSubmit = event => {
        event.preventDefault();

        //axios.post(baseUrl + 'account/login',
        //    {
        //        'userId': this.state.userId,
        //        'password': this.state.password
        //    })
        //    .then(function (response) {
        //        console.log(response);
        //    })
        //    .catch(function (error) {
        //        console.log(error);
        //    });
        this.setState(() => ({
            toDashboard: true
          }));
    }

    render() {
        if (this.state.toDashboard === true) {
            return <Redirect to='/dashboard' />
        }
        
        return (
        <main className="govuk-main-wrapper " id="main-content" role="main">


            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">


                    <h1 className="govuk-heading-xl">Sign in to School experience administration</h1>

                    <p>Lorem ipsum dolor sit amet, gloriatur voluptaria deterruisset vel ex. Mel suscipit insolens constituam in. Mel no simul regione alterum. Et eum audire appareat deserunt, detraxit petentium ne qui, putant delicata ea vim. </p>

                    <div className="govuk-grid-row">
                        <div className="govuk-grid-column-one-half">


                            <fieldset>
                                <legend className="govuk-fieldset__legend govuk-fieldset__legend--l">
                                    <h1 className="govuk-fieldset__heading">
                                        Sign in
                                        </h1>
                                </legend>





                                    <form onSubmit={this.handleSubmit}
                                        className="govuk-fieldset">

                                    <div className="govuk-form-group">
                                        <label className="govuk-label" htmlFor="userId">
                                            User ID
                                            </label>


                                            <input className="govuk-input"
                                                autoFocus
                                                id="userId"
                                                name="userId"
                                                type="text"
                                                value={this.state.userId}
                                                onChange={this.handleChange}
                                                />
                                    </div>




                                    <div className="govuk-form-group">
                                        <label className="govuk-label" htmlFor="password">
                                            Password
                                            </label>


                                            <input className="govuk-input"
                                                id="password"
                                                value={this.state.password}
                                                onChange={this.handleChange}
                                                type="password" />
                                    </div>







                                        <button
                                            className="govuk-button govuk-button--start"
                                            disabled={!this.validateForm()}
                                            type="submit"
                                                >
                                            Sign in
                                        </button>

                                </form>
                            </fieldset>

                        </div>
                    </div>





                    <h2 className="govuk-heading-m">Problems signing in</h2>



                    <details className="govuk-details details-bottomfix">
                        <summary className="govuk-details__summary">
                            <span className="govuk-details__summary-text">
                                Forgotten user ID
                                </span>
                        </summary>
                        <div className="govuk-details__text">
                            <Link to="/signin/forgottenid">Recover your user ID</Link>
                        </div>
                    </details>


                    <details className="govuk-details details-bottomfix">
                        <summary className="govuk-details__summary">
                            <span className="govuk-details__summary-text">
                                Forgotten password
                                </span>
                        </summary>
                        <div className="govuk-details__text">
                                <Link to="/signin/forgottenpassword">Recover your password</Link>
                        </div>
                    </details>

                    <details className="govuk-details details-bottomfix">
                        <summary className="govuk-details__summary">
                            <span className="govuk-details__summary-text">
                                Forgotten user ID and password
                                </span>
                        </summary>
                        <div className="govuk-details__text">
                                <Link to="/signin/forgotten">Recover all your sign in details</Link>
                        </div>
                    </details>






                </div>
            </div>


        </main>          
        )
    }  
}  