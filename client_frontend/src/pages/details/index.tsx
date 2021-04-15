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
import {paintRating} from '../../components/charts/useStyles';
import Radar from "../../components/charts/radarChart";
import List from "../../components/charts/list";
import Research from "../../components/charts/Research";
import BreadCrumbs from "../../components/breadCrumbs/breadCrumbs";
import RatingWrapper, {WrapperModes} from "../../components/ratings/RatingWrapper";
import GeneralInfo from "./GeneralInfo";
import Essentials from "./Essentials";
import RoseNList from "./RoseNList";


const Details = (props: any) => {
  const {
    data,
    status,
    loadDetails,
  } = props;

  const params = useParams() as {id: string};
  const styles = useStyles();
  const isAdvanced = data?.mode === 'advanced';


  useEffect(() => {
    loadDetails(`id=${params.id}`);
  }, []);

  // TODO: refactor layout
  return (
    <div className={styles.root}>
      <Header />
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
            <BreadCrumbs crumbs={['mainSearch', 'results']} />
            <Typography variant={'h5'}>
              Company report
            </Typography>
          </div>
          {/* TODO: refactor these in separate components */}
          <Grid container spacing={3} style={{gap: '5px'}}>
            <GeneralInfo data={data} />
            {/* essentials */}
            <Grid item sm={12}>
              <Essentials data={data} isAdvanced={isAdvanced} />
            </Grid>

            {data.ratingsWindRose && <Grid item sm={12} style={{padding: '0'}}>
              <RoseNList data={data} isAdvanced={isAdvanced} />
            </Grid>}

            {!isAdvanced && <Grid item sm={12} style={{padding: '0'}}>
              <Grid direction={'row'} container style={{gap: '5px'}} wrap={'nowrap'}>
                <RatingWrapper
                  mode={WrapperModes.advanced}
                  title={'Research'}
                  sm={8}
                  style={{display: 'flex', flexDirection: 'column'}}
                >
                  <Research />
                </RatingWrapper>
              </Grid>
            </Grid>}
          </Grid>
        </div>
      </div> : <CircularProgress className={styles.centerLoader} />}
    </div>
  )
}

const connector = () => connect(
  (state: RootState) => ({
    ...state.Details,
  }),
  (dispatch: Dispatch) => ({
    loadDetails: (id: string) => dispatch(CreateAction(
      DetailsActionType.DETAILS_LOAD,
      {url: 'company', params: id})
    ),
  })
)

export default connector()(Details);
