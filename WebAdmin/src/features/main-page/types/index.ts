export type Action = {
  actionid: string;
  name: string;
  eventsource: string;
  status: string;
  esc_period: string;
  pause_suppressed: string;
  notify_if_canceled: string;
  pause_symptoms: string;
};

export type UpdateUserActionsRequest = {
    userId: number;
    actionIds: string[];
}