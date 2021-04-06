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
import BreadCrumbs from "../../components/breadCrumbs";


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
            {/* CRUMBS */}
            <BreadCrumbs
              crumbs={['mainSearch', 'results']}
              styles={styles}
            />
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
            <Grid item sm={12} className={`${styles.paintContainer} ${styles.generalContainer}`}>
              <span className={`${styles.titleFont} ${styles.titleSubFontSize}`}>General</span>
              <Grid container justify={"space-between"}>
                <Grid item sm={4}>
                  <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
                    <span className={styles.paleFont}>Address:</span>
                    <span className={styles.littleFont}>{data.companyGeneral.address}</span>
                  </Grid>
                </Grid>
                <Grid item sm={4}>
                  <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
                    <span className={styles.paleFont}>ID:</span>
                    <div className={`${styles.littleFont} ${styles.flexCol}`}>
                      <span>ID: {data.companyGeneral.id}</span>
                      {data.companyGeneral.lei && <span>LEI: {data.companyGeneral.lei}</span>}
                    </div>
                  </Grid>
                </Grid>
                <Grid item sm={3}>
                  {!!data.companyGeneral.industries.length &&
                  <Grid wrap={'nowrap'} container direction={'row'} className={styles.gap20}>
                    <div><span className={styles.paleFont}>Industry:</span></div>
                    <div
                      className={`${styles.littleFont} ${styles.flexCol}`}
                      style={{lineHeight: '2em'}}
                    >
                      {data.companyGeneral.industries.map((industry: string) =>
                        <span key={industry}>{industry}</span>)}
                    </div>
                  </Grid>}
                </Grid>
              </Grid>
            </Grid>
            {/* essentials */}
            <Grid item sm={12}>
              <Grid container spacing={3} style={{gap: '5px'}} wrap={'nowrap'}>
                {data.essentialRating && <Grid sm={4} item className={styles.paintContainer} style={{maxWidth: '33%'}}>
                  <EssentialRadialRating
                    title={'Essential Rating'}
                    styles={styles}
                    data={data.essentialRating}
                    renderHeaderInfo={renderHeaderInfo}
                  />
                </Grid>}
                {/*advancedForecast advanced*/}
                {/*ratingBars essential*/}
                {/*TODO: advanced total, advanced forecast on advanced mode*/}
                {(data.advancedTotalRating || data.essentialRatingDiversityScore) && <Grid sm={4} item
                       className={styles.paintContainer}
                       style={{maxWidth: '33%'}}
                >
                  <EssentialRadialRating
                    title={isAdvanced ? 'Advanced Total Rating' : "Essential Diversity Rating"}
                    styles={styles}
                    data={data[isAdvanced ? 'advancedTotalRating' : 'essentialRatingDiversityScore']}
                    renderHeaderInfo={renderHeaderInfo}
                  />
                </Grid>}
                {(data.essentialRatingEquityAndInclusionScore || data.advancedForecastRating) && <Grid
                  item
                  sm={4}
                  className={`${styles.paintContainer}`}
                  style={{maxWidth: '33%'}}
                >
                  <EssentialRadialRating
                    title={isAdvanced ? 'Advanced Forecast Rating' : 'Essential Equity & Inclusion Rating'}
                    styles={styles}
                    data={data[isAdvanced ? 'advancedForecastRating' : 'essentialRatingEquityAndInclusionScore']}
                    renderHeaderInfo={renderHeaderInfo}
                  />
                </Grid>}
              </Grid>
            </Grid>

            {data.ratingsWindRose && <Grid item sm={12} style={{padding: '0'}}>
              <Grid direction={'row'} container style={{gap: '5px'}} wrap={'nowrap'}>
                <AdvancedRatingWrapper
                  title={`${isAdvanced ? 'Advanced' : 'Essential'} Rating`}
                  data={data.ratingsWindRose}
                  sm={8}
                >
                  <Radar data={data.ratingsWindRose} />
                </AdvancedRatingWrapper>
                {(data.ratingBars || data.ratingsWindRose) && <AdvancedRatingWrapper
                  title={`${isAdvanced ? 'Advanced' : 'Essential'} Sub Scores`}
                  data={isAdvanced ? data.ratingsWindRose : data.ratingBars}
                  justify={'space-between'}
                  sm={4}
                >
                  <List
                    classes={styles.littleFont}
                    data={isAdvanced ? data.ratingsWindRose : data.ratingBars}
                  />
                </AdvancedRatingWrapper>}
              </Grid>
            </Grid>}

            {!isAdvanced && <Grid item sm={12} style={{padding: '0'}}>
              <Grid direction={'row'} container style={{gap: '5px'}} wrap={'nowrap'}>
                <AdvancedRatingWrapper
                  title={'Research'}
                  sm={8}
                  style={{display: 'flex', flexDirection: 'column'}}
                >
                  <Research />
                </AdvancedRatingWrapper>
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
