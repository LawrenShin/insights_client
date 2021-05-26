import React, {useEffect} from 'react';
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {State} from "../../store/createReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {RequestStatuses} from "../../api/requestTypes";
import {LinearProgress} from "@material-ui/core";
import {useIndustruOptionStyles} from "./useStyles";
import {Rounded} from "../button";
import {ResultsActionType} from "../../pages/results/duck";
import {useHistory} from "react-router-dom";
import {renderOptions} from "../../pages/details/helpers";

interface StateProps {
  industries: State;
}
interface DispatchProps {
  getDictionaries: () => void;
  resultsRequest: () => void;
}
interface Props extends StateProps, DispatchProps {}

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
