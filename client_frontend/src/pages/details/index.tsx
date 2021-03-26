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


const Details = (props: any) => {
  const {
    data,
    status,
    loadDetails,
  } = props;

  const params = useParams() as {id: string};
  const styles = useStyles();

  const renderHeaderInfo = (key: string, value: string) => <Grid item>
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
          <Grid container spacing={3}>
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
