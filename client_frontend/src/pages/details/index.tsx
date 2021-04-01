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
import EssentialRadialRating from "../../components/ratings/EssentialRating";
import {paintRating} from "../results/prepareForGrid";
import AdvancedRatingWrapper from "../../components/ratings/AdvancedRatingWrapper";
import Radar from "../../components/charts/radarChart";
import List from "../../components/charts/list";
import Research from "../../components/charts/Research";


const renderValue = (value: string | number | boolean) => <span>
    {typeof value === "boolean" ?
      value ? 'Yes' : 'No'
      : value}
  </span>

const renderHeaderInfo = (key: string, value: string) => <Grid item key={`${value}${key}`}>
  <Grid direction={'column'} container>
    <span>{keyTitle(key)}</span>
    {
      key === 'Rating' ? <Grid container direction={'row'} alignItems={'center'}>
          <div className={paintRating(value)}></div>
          {renderValue(value)}
        </Grid>
        :
        <>{renderValue(value)}</>
    }
  </Grid>
</Grid>;


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
            <span className={styles.purpColor}>Main search</span>
            <Typography variant={'h5'}>
              Company report
            </Typography>
          </div>
          {/* TODO: refactor these in separate components */}
          <Grid container spacing={3} style={{gap: '5px'}}>
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
            {/*general and essentials*/}
            <Grid item sm={12}>
              <Grid container spacing={3} style={{gap: '5px'}} wrap={'nowrap'}>
                <Grid item sm={4} className={`${styles.paintContainer}`} style={{maxWidth: '33%'}}>
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
                {data.essentialRating && <Grid sm={4} item className={styles.paintContainer} style={{maxWidth: '33%'}}>
                  <EssentialRadialRating
                    title={'Essential rating'}
                    styles={styles}
                    data={data.essentialRating}
                    renderHeaderInfo={renderHeaderInfo}
                  />
                </Grid>}
                {/*advancedForecast advanced*/}
                {/*ratingBars essential*/}
                {data && (isAdvanced ? <Grid sm={4} item
                    className={styles.paintContainer}
                    style={{maxWidth: '33%'}}
                  >
                    <EssentialRadialRating
                      title={keyTitle('advancedForecast')}
                      styles={styles}
                      data={data.advancedForecastRating}
                      renderHeaderInfo={renderHeaderInfo}
                    />
                  </Grid>
                    :
                  <AdvancedRatingWrapper
                    title={'Essential Sub scores'}
                    data={data.ratingsWindRose}
                    justify={'space-between'}
                    sm={4}
                  >
                    <List
                      classes={styles.overflowBarList}
                      data={data.ratingBars}
                    />
                  </AdvancedRatingWrapper>
                )}
              </Grid>
            </Grid>
          {/* essential 2d line */}
          {!isAdvanced && <Grid item sm={12} style={{padding: '0'}}>
              <Grid direction={'row'} container style={{gap: '5px'}} wrap={'nowrap'}>
                <AdvancedRatingWrapper
                  justify={'space-between'}
                  sm={4}
                >
                  <EssentialRadialRating
                    title={"Essential diversity rating"}
                    styles={styles}
                    data={data.essentialRatingDiversityScore}
                    renderHeaderInfo={renderHeaderInfo}
                  />
                </AdvancedRatingWrapper>
                <AdvancedRatingWrapper
                  justify={'space-between'}
                  sm={4}
                >
                  <EssentialRadialRating
                    title={'Essential equity & inclusion rating '}
                    styles={styles}
                    data={data.essentialRatingEquityAndInclusionScore}
                    renderHeaderInfo={renderHeaderInfo}
                  />
                </AdvancedRatingWrapper>
                <AdvancedRatingWrapper
                  title={'Research'}
                  sm={4}
                  style={{display: 'flex', flexDirection: 'column'}}
                >
                  <Research />
                </AdvancedRatingWrapper>
              </Grid>
            </Grid>}

          {/* advanced */}
            {data.ratingsWindRose && <Grid item sm={12} style={{padding: '0'}}>
              <Grid direction={'row'} container style={{gap: '5px'}} wrap={'nowrap'}>
                <AdvancedRatingWrapper
                  title={'Advanced rating'}
                  data={data.ratingsWindRose}
                  sm={8}
                >
                  <Radar data={data.ratingsWindRose} />
                </AdvancedRatingWrapper>
                {isAdvanced && <AdvancedRatingWrapper
                  title={'Advanced Sub scores'}
                  data={data.ratingsWindRose}
                  justify={'space-between'}
                  sm={4}
                >
                  {/*TODO: ask what data should be displayed rose or bars*/}
                  <List data={data.ratingsWindRose}/>
                </AdvancedRatingWrapper>}
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
