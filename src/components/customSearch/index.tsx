import React from 'react';
import useStyles from "./useStyles";
import IndustriesOptions from "./industriesOptions";

interface OwnProps {
  tab: string;
  setTab: (tab: string) => void;
}
interface Props extends OwnProps {}


const CustomSearch  = ({ tab }: Props) => {
  const styles = useStyles();

  return (
    <div className={styles.container}>
      {/* @ts-ignore*/}
      {tab === 'industry' && <IndustriesOptions />}
    </div>
  )
}

export default CustomSearch;
