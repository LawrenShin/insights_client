import React from 'react';
import {useParams} from "react-router-dom";
import useStyles from "./useStyles";
import Header from "../../components/Header";
import {Typography} from "@material-ui/core";


const Details = (props: any) => {
  const params = useParams();
  const styles = useStyles();
  console.log(params, 'params in deets');

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.content}>
        <div className={styles.list}>
          <ul>
            <li className={styles.listSelected}>Key information</li>
            <li>Key information</li>
          </ul>
        </div>
        <div className={styles.graphs}>
          <div>
            <span >Main search</span>
            <Typography variant={'h5'}>
              Company details
            </Typography>
          </div>
        </div>
      </div>
    </div>
  )
}

export default Details;
