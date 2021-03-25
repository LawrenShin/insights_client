import React from 'react';
import {useParams} from "react-router-dom";
import useStyles from "./useStyles";
import Header from "../../components/Header";
import {Typography} from "@material-ui/core";
import {connect} from "react-redux";
import {RootState} from "../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {DetailsActionType} from "./duck";


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
            {/* TODO: later provide selection */}
            <li className={styles.listSelected}>Key information</li>
          </ul>
        </div>

        <div className={styles.graphs}>
          <div>
            <span>Main search</span>
            <Typography variant={'h5'}>
              Company details
            </Typography>
          </div>
        </div>
      </div>
    </div>
  )
}

const connector = () => connect(
  (state: RootState) => ({
    // ...state.details,
  }),
  (dispatch: Dispatch) => ({
    loadDetails: (id: string) => dispatch(CreateAction(
      DetailsActionType.DETAILS_LOAD,
      {url: 'company', id})
    ),
  })
)

export default connector()(Details);
