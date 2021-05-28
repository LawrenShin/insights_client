import React from 'react';
import useStyles from "./useStyles";
import IndustriesOptions from "./industriesOptions";
import CountryOptions from "./CountryOptions";

interface OwnProps {
  tab: string;
  saveTab: (tab: string) => void;
}
interface Props extends OwnProps {}


const CustomSearch  = ({ tab }: Props) => {
  const styles = useStyles();

  return (
    <div className={styles.container}>
      {/* TODO: dude fix this */}
      {/* @ts-ignore*/}
      {tab === 'industry' && <IndustriesOptions />}
      {/* @ts-ignore*/}
      {tab === 'country' && <CountryOptions />}
    </div>
  )
}

export default CustomSearch;
