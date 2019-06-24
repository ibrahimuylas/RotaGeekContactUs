import axios from "axios";

const requestContactUsType = 'REQUEST_CONTACT_US';
const receiveContactUsType = 'RECEIVE_CONTACT_US';
const initialState = { name: '', email: '', message: '', hasSent: false, isLoading: false };

export const actionCreators = {
  send: (name, email, message) => async (dispatch, getState) => {

    dispatch({ type: requestContactUsType, name, email, message });

    await axios.post('http://localhost:5001/api/contactus/add', {name, email, message})
      .then(response => {
          dispatch({ type: receiveContactUsType, hasSent: true});
      }).catch(err => {
           if(err.response !== undefined && err.response.status !== undefined && err.response.status === 500)
               dispatch({ type: receiveContactUsType, errorMessage: err.response.data });
          else
               dispatch({ type: receiveContactUsType, errorMessage: err});
      });
      
  }
};

export const reducer = (state = initialState, action) => {

  if (action.type === requestContactUsType) {
    return {
      ...state,
        name: action.name,
        email: action.email,
        message: action.message,
        hasSent: action.hasSent,
        errorMessage: action.errorMessage,
        isLoading: true
    };
  }

  if (action.type === receiveContactUsType) {
    return {
      ...state,
        name: action.name,
        email: action.email,
        message: action.message,
        hasSent: action.hasSent,
        errorMessage: action.errorMessage,
        isLoading: false
    };
  }

  return state;
};
