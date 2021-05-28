import React, {useEffect, useState} from 'react';
import {Grid} from "@material-ui/core";
import {useContryOptionsStyles} from "./useStyles";
import {OptionType, renderOptions} from "../../pages/details/helpers";
import ArrowForwardIosIcon from '@material-ui/icons/ArrowForwardIos';
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {ResultsActionType} from "../../pages/results/duck";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import ExpandLessIcon from '@material-ui/icons/ExpandLess';

// TODO: Both options components should be refactored into one component.

const CountryOptions =
  ({
     data,
     getDictionaries,
     resultsRequest,
  }: any) => {
  const styles = useContryOptionsStyles();
  const [stateRegion, setStateRegion] = useState<number | null>(null);
  const [stateSubRegion, setStateSubRegion] = useState<number | null>(null);

  useEffect(() => {
    if (!data) getDictionaries();
  }, []);

  return (
    <Grid container>
      <Grid item>
        <Grid container className={styles.searchCriteriasContainer}>
          <Grid item>
            <div className={styles.colTitle}>
              <span>Search Criteria</span>
            </div>
            <div className={styles.line}></div>
            <span className={styles.colSubTitle}>Region</span>
            <Grid container className={styles.regionsContainer}>
              {
                data?.regions && renderOptions(
                  data.regions.map((region: OptionType) => {
                    const {children, id, name} = region;
                    return {id, name};
                  }),
                  () => <div className={styles.arrowContainer}>
                  <ArrowForwardIosIcon style={{ fontSize: '.8em'}} />
                </div>,
                (id: number) => setStateRegion(id === stateRegion ? null : id))
              }
            </Grid>
          </Grid>
          <Grid item>
            <div className={styles.colTitle}>
              <span>Subregion/Country</span>
            </div>
            <div className={styles.line}></div>
            <span className={styles.colSubTitle}>By subregions category</span>
            <Grid container>
              {
                (data?.regions && stateRegion) && renderOptions(
                  stateSubRegion ?
                    data.regions.filter((region: OptionType) => region.id === stateRegion)
                    :
                    data.regions.filter((region: OptionType) => region.id === stateRegion)
                      .map((region: OptionType) => {
                        const {children, id, name} = region;
                        return {id, name};
                      }),
                  () => <div className={styles.arrowContainer}>
                      {stateSubRegion ?
                          <ExpandMoreIcon style={{fontSize: '.8em'}}/>
                          :
                          <ExpandLessIcon style={{fontSize: '.8em'}}/>}
                    </div>
                  ,
                  (id: number) => setStateSubRegion(id === stateSubRegion ? null : id))
              }
            </Grid>
          </Grid>
        </Grid>
      </Grid>
      <Grid item> </Grid>
    </Grid>
  );
}

export default connect(
  ({Dictionaries}: RootState) => ({
    ...Dictionaries,
  }),
  (dispatch: Dispatch, state: RootState) => ({
    getDictionaries: () => dispatch(CreateAction('DICTIONARIES_LOAD', {url: 'dictionaries'})),
    // resultsRequest: () => dispatch(
      // CreateAction(ResultsActionType.RESULTS_LOAD, {url: 'compan', payload: {}})
    // ),
  })
)(CountryOptions);