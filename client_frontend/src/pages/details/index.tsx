import React from 'react';
import {useLocation} from "react-router-dom";


const Details = (props: any) => {
  const location = useLocation();
  console.log(location.state, 'state in deets');

  return (<>
    Deets
  </>)
}

export default Details;
