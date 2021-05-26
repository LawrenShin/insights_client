import React, {useState} from 'react';
import LookupSearch from "../../components/lookupSearch";
import CustomSearch from "../../components/customSearch";
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {useStyles as useWelcomeStyled} from '../../components/customSearch/useStyles';
import Welcome from "../../components/forms/register/steps/welcome";
import {connect} from "react-redux";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {ResultsActionType} from "../results/duck";
import {RootState} from "../../store/rootReducer";

interface Props {
  tab: string,
  saveTab: (tab: string) => void
};

const SearchCompanies = ({
  saveTab,
  tab,
}: Props) => {
  const styles = useStyles();
  const styles2 = useWelcomeStyled();

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>
        <div className={styles.searchContainer}>
          <LookupSearch tab={tab} saveTab={saveTab} />
        </div>
        <div
          className={`
          ${tab !== 'country' ? styles.searchSettingsContainer : ''} 
          ${tab === 'company' ? styles.centerContent : ''}`
        }>
          {/* TODO: some day reconsider this cas custom might also be on companies */}
          {tab !== 'company' && <CustomSearch tab={tab} saveTab={saveTab} />}
          {tab === 'company' && <Welcome classes={styles2} />}
        </div>
      </div>
    </div>
  )
}

export default connect(
  ({Results}: RootState) => ({
    tab: Results.tab,
  }),
  (dispatch: Dispatch) => ({
    saveTab: (tab: string) => dispatch(CreateAction(ResultsActionType.RESULTS_SET_TAB, tab)),
  })
)(SearchCompanies);
