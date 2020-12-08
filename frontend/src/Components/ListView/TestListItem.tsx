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
                <label>#{test.Id}</label>
                <label>{test.Temperature.toPrecision(3)} °C</label>
                <label>{test.HasFever ? "Fever" : "No fever"}</label>
                <label>{Moment(test.TimeOfDataRecording).format('d MMM YYYY')}</label>
            </div>
        );
}