import React, {useEffect} from 'react';
import {StyledCheckbox} from "../checkbox";
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {State} from "../../store/createReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {RequestStatuses} from "../../api/requestTypes";
import {Grid, LinearProgress} from "@material-ui/core";
import {useIndustruOptionStyles} from "./useStyles";
import {Rounded} from "../button";
import {LookupSearchActionType} from "../lookupSearch/duck";
import {ResultsActionType} from "../../pages/results/duck";
import {useHistory} from "react-router-dom";

// industry
type IndustryOption = {
  id: number,
  name: string,
  children?: IndustryOption[],
}
interface IndustryOptionProps {
  industry: IndustryOption,
  options: number[],
  saveIndustryOptions: (checked: boolean, id: number) => void,
}
// industries
interface StateProps {
  industries: State;
}
interface DispatchProps {
  getDictionaries: () => void;
  resultsRequest: () => void;
}
interface Props extends StateProps, DispatchProps {}


const IndustryOption = connect(
  (state: RootState) => ({
    options: state.LookupSearch.data.options
  }),
  (dispatch: Dispatch) => ({
    saveIndustryOptions: (checked: boolean, id: number) =>
      dispatch(CreateAction(LookupSearchActionType.SAVE_INDUSTRY_OPTIONS, {checked, id})),
  })
)(({
   industry: {id, name, children},
   saveIndustryOptions,
}: IndustryOptionProps) => {
  const styles = useIndustruOptionStyles();

  const renderRow = (name: string) => <Grid
    container
    alignItems={'center'}
    className={!children ? styles.marginLeft15 : styles.parent}
  >
    <div><StyledCheckbox onChange={(e) => saveIndustryOptions(e.target.checked, id)} /></div>
    <div><span>{name}</span></div>
  </Grid>

  return <>
    {renderRow(name)}
    {children && renderOptions(children)}
  </>
});


const renderOptions = (industries: IndustryOption[]) =>
  industries.map(industry => <IndustryOption key={industry.id} industry={industry} />);


const IndustriesOptions = (props: Props) => {
  const styles = useIndustruOptionStyles();
  const {
    industries,
    getDictionaries,
    resultsRequest,
  } = props;
  const history = useHistory();

  useEffect(() => {
    if (!industries.data) getDictionaries();
  }, []);

  return (
    (industries.status !== RequestStatuses.loading && industries.data !== null) ?
      <div className={styles.root}>
        <div className={styles.searchTitle}>Search Criteria</div>
        <div className={styles.list}>
          {industries.data && renderOptions(industries.data.industries)}
          <br/>
          <Rounded onClick={() => {
            resultsRequest();
            history.push('/results');
          }}>View results</Rounded>
        </div>
      </div>
    :
      <LinearProgress />
  );
}

export default connect(
  (state: RootState) => ({
    industries: state.Dictionaries,
  }),
  (dispatch: Dispatch, state: RootState) => ({
    getDictionaries: () => dispatch(CreateAction('DICTIONARIES_LOAD', {url: 'dictionaries'})),
    resultsRequest: () => dispatch(
        CreateAction(ResultsActionType.RESULTS_LOAD, {url: 'industries', payload: {}})
    ),
  })
)(IndustriesOptions);
