import React, {useEffect} from 'react';
import {StyledCheckbox} from "../checkbox";
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {State} from "../../store/createReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {RequestStatuses} from "../../api/requestTypes";
import {CircularProgress, Grid, LinearProgress} from "@material-ui/core";
import {useIndustruOptionStyles} from "./useStyles";
import {Rounded} from "../button";


type IndustryOption = {
  id: number;
  name: string;
  children?: IndustryOption[];
}

interface StateProps {
  industries: State;
}
interface DispatchProps {
  getDictionaries: () => void;
}
interface Props extends StateProps, DispatchProps {}



const IndustryOption = ({industry: {id, name, children}}: { industry: IndustryOption }) => {
  const styles = useIndustruOptionStyles();

  const renderRow = (name: string) => <Grid
    container
    alignItems={'center'}
    className={!children ? styles.marginLeft15 : styles.parent}
  >
    <div><StyledCheckbox /></div>
    <div><span>{name}</span></div>
  </Grid>

  return <>
    {renderRow(name)}
    {children && renderOptions(children)}
  </>
}

const renderOptions = (industries: IndustryOption[]) =>
  industries.map(industry => <IndustryOption key={industry.id} industry={industry} />);

const IndustriesOptions = ({
    industries,
    getDictionaries,
  }: Props) => {
  const styles = useIndustruOptionStyles();

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
          <Rounded>View results</Rounded>
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
  (dispatch: Dispatch) => ({
    getDictionaries: () => dispatch(CreateAction('DICTIONARIES_LOAD', {url: 'dictionaries'})),
  })
)(IndustriesOptions);
