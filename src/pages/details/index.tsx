import React, {useEffect} from 'react';
import {useParams} from "react-router-dom";
import useStyles from "./useStyles";
import Header from "../../components/Header";
import {CircularProgress, Grid, Typography} from "@material-ui/core";
import {connect} from "react-redux";
import {RootState} from "../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {DetailsActionType} from "./duck";
import {RequestStatuses} from "../../api/requestTypes";
import BreadCrumbs from "../../components/breadCrumbs/breadCrumbs";
import Error from '../../components/Error';
import {Content as CompanyContent} from "../../components/details/company/content";
import {Content as IndustryContent} from "../../components/details/industry/content";
import {keyTitle} from "../../helpers";


const Details = (props: any) => {
  const {
    data,
    status,
    loadDetails,
  } = props;

  const params = useParams() as {id: string};
  const styles = useStyles();
  const tab = localStorage.getItem('tab');


  useEffect(() => {
    loadDetails(`id=${params.id}`);
  }, []);
  // TODO: refactor layout
  try {
    return (
      <div className={styles.root}>
        <Header/>
        {(status === RequestStatuses.still && data) ? <div className={styles.content}>
          <div className={`${styles.list} ${styles.paintContainer}`}>
            <ul>
              {/* TODO: later provide selection */}
              <li className={styles.listSelected}>Key information</li>
            </ul>
          </div>

          <div className={styles.width100}>
            <div className={styles.contentTitle}>
              {/* CRUMBS */}
              <BreadCrumbs crumbs={['mainSearch', 'results']}/>
              <Typography variant={'h5'}>
                {tab && `${keyTitle(tab)} report`}
              </Typography>
            </div>
            {tab === 'company' && <CompanyContent data={data} />}
            {tab === 'industry' && <IndustryContent data={data} />}
          </div>
        </div> : <CircularProgress className={styles.centerLoader}/>}
      </div>
    )
  } catch (e) {
    return <Error error={e} />;
  }
}

const connector = () => connect(
  (state: RootState) => ({
    ...state.Details,
  }),
  (dispatch: Dispatch) => ({
    loadDetails: (id: string) => dispatch(CreateAction(
      DetailsActionType.DETAILS_LOAD,
      {url: localStorage.getItem('tab'), params: id})
    ),
  })
)

export default connector()(Details);
