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
import {keyTitle} from "../../helpers";
import {RequestStatuses} from "../../api/requestTypes";
import {Rounded} from "../../components/button";
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import EssentialRating from "../../components/ratings/EssentialRating";


const Details = (props: any) => {
  const {
    data,
    status,
    loadDetails,
  } = props;

  const params = useParams() as {id: string};
  const styles = useStyles();

  const renderHeaderInfo = (key: string, value: string) => <Grid item key={`${value}${key}`}>
    <Grid direction={'column'} container>
      <span>{keyTitle(key)}</span>
      <span>
        {
          typeof value === "boolean" ?
            value ? 'Yes' : 'No'
            : value
        }
      </span>
    </Grid>
  </Grid>;


  useEffect(() => {
    loadDetails(`id=${params.id}`);
  }, []);
  console.log(params, 'params in deets');

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
            <span className={styles.purpColor}>Main search</span>
            <Typography variant={'h5'}>
              Company report
            </Typography>
          </div>
          {/* TODO: refactor these in separate components */}
          <Grid container spacing={5} style={{gap :'5px'}}>
            {/* header container */}
            <Grid item sm={12} className={styles.paintContainer}>
              <Grid direction={'row'} container >
                <Grid item className={styles.companyHeader}>
                  <Grid direction={'row'} container>
                    <span className={styles.titleFont}> {data.companyHeader.name} </span>
                    {
                      Object.keys(data.companyHeader).map((key) => {
                        if (key !== 'name') return renderHeaderInfo(key, data.companyHeader[key])
                      })
                    }
                  </Grid>
                </Grid>
                <Rounded>EXPORT<ExpandMoreIcon /></Rounded>
              </Grid>
            </Grid>

            <Grid item sm={4} className={`${styles.paintContainer}`} style={{maxWidth: '307px'}}>
              <span className={`${styles.titleFont} ${styles.titleSubFontSize}`}>General</span>
              <Grid item>
                <Grid container >
                  <Grid item>
                    <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
                      <span className={styles.paleFont}>Address:</span>
                      <span>{data.companyGeneral.address}</span>
                    </Grid>
                    <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
                      <span className={styles.paleFont}>ID:</span>
                      <ul style={{listStyle: 'none'}}>
                        <li><span>ID: {data.companyGeneral.id}</span></li>
                        <li><span>LEI: {data.companyGeneral.lei}</span></li>
                      </ul>
                    </Grid>
                    <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
                      <span className={styles.paleFont}>Industries:</span>
                      <ul style={{listStyle: 'none'}}>
                        {/*<li><span>TODO:assumed empty for now</span></li>*/}
                      </ul>
                    </Grid>
                  </Grid>
                </Grid>
              </Grid>
            </Grid>
            {data.essentialRating && <Grid sm={4} item className={styles.paintContainer}>
              <EssentialRating
                title={'Essential rating'}
                styles={styles}
                data={data.essentialRating}
                renderHeaderInfo={renderHeaderInfo}
              />
            </Grid>}
            {data.essentialRatingDiversityScore && <Grid sm={4} item className={styles.paintContainer}>
              <EssentialRating
                title={'Essential rating diversity score'}
                styles={styles}
                data={data.essentialRatingDiversityScore}
                renderHeaderInfo={renderHeaderInfo}
              />
            </Grid>}
          </Grid>
        </div>
      </div> : <CircularProgress />}
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
