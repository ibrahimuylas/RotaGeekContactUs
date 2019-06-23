import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/ContactUs';
import WallClock from './WallClock';

class ContactUs extends Component {

  componentDidMount() {
    // This method is called when the component is first added to the document
     
  }

  handleSubmit = event => {
      event.preventDefault();
      this.props.send(this.refs.name.value, this.refs.email.value, this.refs.message.value);
  }

    renderSendMessage() {
    if (this.props.hasSent)
        return (
            <div class="alert alert-info mb-2" role="alert">
                Your message has been sent. We will get in touch with you asap.
            </div>
        );
    return '';
    }

    renderErrorMessage() {
        if (this.props.errorMessage)
            return (
                <div class="alert alert-warning mb-2" role="alert">
                    {this.props.errorMessage}
                </div>
            );
        return '';
    }

render() {
  console.log(this.state);
    return (
      <div>
            <WallClock />
            <h1>Contact Us</h1>
            <form onSubmit={this.handleSubmit}>
                {this.renderSendMessage()}
                {this.renderErrorMessage()}
                <div className="form-group">
                    <label htmlFor="name">Name</label>
                    <input type="text" className="form-control" id="name" ref="name" defaultValue={this.props.name} />
                </div>
                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input type="email" className="form-control" id="email" ref="email" placeholder="example@example.com" defaultValue={this.props.email} />
                </div>
                <div className="form-group">
                    <label htmlFor="message">Message</label>
                    <textarea className="form-control" id="message" ref="message" rows="3" defaultValue={this.props.message} ></textarea>
                </div>
                <button type="submit" className="btn btn-outline-primary">Submit</button>
            </form>
      </div>
    );
  }
}

export default connect(
  state => state.contactUs,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(ContactUs);
