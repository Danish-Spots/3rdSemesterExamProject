import React from 'react'
import Test from '../../classes/Test';
import "../../css/tests.scss";
import Moment from 'moment';

interface TestListItemProps {
        test:Test;
}

export const TestListItem: React.FC<TestListItemProps> = ({test}) => {
    Moment.locale('en')    
    
    return (
           <div className={`${test.HasFever ? "fever" : "noFever"} test-container`}>
                <label style={{width:"60px"}} >#{test.Id}</label>
                <label className="testInfo">{test.Temperature.toPrecision(4)} °C</label>
                <label className="testInfo">{test.HasFever ? "Fever" : "No fever"}</label>
                <label>{Moment(test.TimeOfDataRecording).format('d MMM YYYY')}</label>
            </div>
        );
}