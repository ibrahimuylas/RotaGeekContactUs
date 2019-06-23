
import React  from 'react';
import Clock from 'react-live-clock';
 
class WallClock extends React.Component {
    render() {
        return <Clock format={'HH:mm:ss'} ticking={true} style={{ float: 'right'}} />;
    }
}

export default WallClock;